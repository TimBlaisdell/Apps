using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ChaosGame {
    public static class Extensions {
        public static bool RGBEquals(this Color c1, Color c2) {
            return c1.R == c2.R && c1.G == c2.G && c1.B == c2.B;
        }
        public static bool ARGBEquals(this Color c1, Color c2) {
            return c1.A == c2.A && c1.R == c2.R && c1.G == c2.G && c1.B == c2.B;
        }
        public static float DistanceTo(this PointF p1, PointF p2) {
            return (float) Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
        public static bool EqualTo(this PointF p1, PointF p2, float tolerance = 0.001F) {
            return Math.Abs(p1.X - p2.X) < tolerance && Math.Abs(p1.Y - p2.Y) < tolerance;
        }
        public class IterationResult {
            public IterationResult Reset() {
                Color = null;
                Abort = false;
                return this;
            }
            public bool Abort;
            public Color? Color;
        }
        /// <summary>
        ///     Iterate over the bitmap, calling the specified function for each pixel.  Function must be defined as
        ///     void Func(int x, int y, byte a, byte r, byte g, byte b, int index, IterationResult itres)
        /// </summary>
        public static void IterateOver(this Bitmap bmp, Action<int, int, byte, byte, byte, byte, int, IterationResult> p) {
            bmp.IterateOver(new Rectangle(0, 0, bmp.Width, bmp.Height), p);
        }
        /// <summary>
        ///     Iterate over an area of the bitmap, calling the specified function for each pixel.  Function must be defined as
        ///     void Func(int x, int y, byte a, byte r, byte g, byte b, int index, IterationResult itres)
        /// </summary>
        public static void IterateOver(this Bitmap bmp, Rectangle rect, Action<int, int, byte, byte, byte, byte, int, IterationResult> p) {
            // Lock the bitmap's bits.
            var bmprect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            rect = Rectangle.Intersect(rect, bmprect);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;
            // Declare an array to hold the bytes of the bitmap.
            int bytes = bmpData.Stride * rect.Height;
            //if (_rgbValues == null || _rgbValues.Length < bytes) {
            //    _rgbValues = new byte[bytes];
            //}
            byte[] rgbValues = new byte[bytes];
            // Copy the RGB values into the array.
            Marshal.Copy(ptr, rgbValues, 0, bytes);
            //int count = 0;
            int stride = bmpData.Stride;
            bool changedSomething = false;
            var itres = new IterationResult();
            rect = Rectangle.Intersect(rect, bmprect);
            int i = 0;
            for (int y = 0; y < rect.Height; y++) {
                for (int x = 0; x < rect.Width; x++) {
                    byte b = rgbValues[(y * stride) + (x * 4)];
                    byte g = rgbValues[(y * stride) + (x * 4) + 1];
                    byte r = rgbValues[(y * stride) + (x * 4) + 2];
                    byte a = rgbValues[(y * stride) + (x * 4) + 3];
                    p(x + rect.Left, y + rect.Top, a, r, g, b, i++, itres.Reset());
                    if (itres.Color != null) {
                        changedSomething = true;
                        var color = itres.Color.Value;
                        rgbValues[(y * stride) + (x * 4)] = color.B;
                        rgbValues[(y * stride) + (x * 4) + 1] = color.G;
                        rgbValues[(y * stride) + (x * 4) + 2] = color.R;
                        rgbValues[(y * stride) + (x * 4) + 3] = color.A;
                    }
                    if (itres.Abort) break;
                }
                if (itres.Abort) break;
            }
            if (changedSomething) Marshal.Copy(rgbValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
        }
    }
}