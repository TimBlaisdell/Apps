using System.Drawing;
using JSON;

namespace BackgroundSwitcher {
    public static class Extensions {
        public static double Ratio(this RectangleF rect) {
            return rect.Height / rect.Width;
        }
        public static double Ratio(this Rectangle rect) {
            return (double) rect.Height / rect.Width;
        }
        public static Rectangle OffsetBy(this Rectangle rect, int x, int y) {
            return new Rectangle(rect.X + x, rect.Y + y, rect.Width, rect.Height);
        }
        public static RectangleF OffsetBy(this RectangleF rect, int x, int y) {
            return new RectangleF(rect.X + x, rect.Y + y, rect.Width, rect.Height);
        }
        public static JSONObject toJSON(this Rectangle rect) {
            return new JSONObject().put("X", rect.X).put("Y", rect.Y).put("Width", rect.Width).put("Height", rect.Height);
        }
        public static JSONObject toJSON(this RectangleF rect) {
            return new JSONObject().put("X", rect.X).put("Y", rect.Y).put("Width", rect.Width).put("Height", rect.Height);
        }
    }
}