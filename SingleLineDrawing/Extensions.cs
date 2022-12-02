using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace SingleLineDrawing {
    public static class Extensions {
        public static Rectangle BoundingRect(this RectangleF rect) => new Rectangle((int) Math.Floor(rect.X), (int) Math.Floor(rect.Y), (int) Math.Ceiling(rect.Width), (int) Math.Ceiling(rect.Height));
        public static double DistanceTo(this Point p1, Point p2) {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
        public static void IterateOver(this Bitmap bmp, LockData data, Func<int, int, byte, byte, byte, byte, int, byte[]> p) {
            bmp.IterateOver(data, new Rectangle(0, 0, bmp.Width, bmp.Height), p);
        }
        /// <summary>
        ///     Function parameters are: x, y, a, r, g, b, index.  Function can return an array of bytes ordered as [a, r, g, b] to
        ///     effect a change.
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="data"></param>
        /// <param name="bounds">Region within bitmap to iterate over.  Can extend outside the bitmap's area.</param>
        /// <param name="p"></param>
        public static void IterateOver(this Bitmap bmp, LockData data, Rectangle bounds, Func<int, int, byte, byte, byte, byte, int, byte[]> p) {
            bounds = Rectangle.Intersect(bounds, new Rectangle(0, 0, bmp.Width, bmp.Height));
            var ld = (LockDataP) data;
            byte[] rtn;
            for (int y = bounds.Top; y < bounds.Bottom; y++) {
                for (int x = bounds.Left; x < bounds.Right; x++) {
                    byte b = ld.rgbValues[(y * ld.stride) + (x * 4)];
                    byte g = ld.rgbValues[(y * ld.stride) + (x * 4) + 1];
                    byte r = ld.rgbValues[(y * ld.stride) + (x * 4) + 2];
                    byte a = ld.rgbValues[(y * ld.stride) + (x * 4) + 3];
                    rtn = p(x, y, a, r, g, b, (y * ld.stride) + (x * 4));
                    if (rtn != null) {
                        ld.changedSomething = true;
                        ld.rgbValues[(y * ld.stride) + (x * 4)] = rtn[3];
                        ld.rgbValues[(y * ld.stride) + (x * 4) + 1] = rtn[2];
                        ld.rgbValues[(y * ld.stride) + (x * 4) + 2] = rtn[1];
                        ld.rgbValues[(y * ld.stride) + (x * 4) + 3] = rtn[0];
                    }
                }
            }
        }
        public static LockData Lock(this Bitmap bmp) {
            var data = new LockDataP();
            // Lock the bitmap's bits.
            data.bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            // Get the address of the first line.
            data.ptr = data.bmpData.Scan0;
            // Declare an array to hold the bytes of the bitmap.
            data.bytes = data.bmpData.Stride * bmp.Height;
            data.rgbValues = new byte[data.bytes];
            // Copy the RGB values into the array.
            Marshal.Copy(data.ptr, data.rgbValues, 0, data.bytes);
            //int count = 0;
            data.stride = data.bmpData.Stride;
            data.changedSomething = false;
            return data;
        }
        public static void Unlock(this Bitmap bmp, LockData data) {
            var ld = (LockDataP) data;
            if (ld.changedSomething) Marshal.Copy(ld.rgbValues, 0, ld.ptr, ld.bytes);
            bmp.UnlockBits(ld.bmpData);
        }
        public class LockData {
        }
        private class LockDataP : LockData {
            public BitmapData bmpData;
            public int bytes;
            public bool changedSomething;
            public IntPtr ptr;
            public byte[] rgbValues;
            public int stride;
        }
    }
}