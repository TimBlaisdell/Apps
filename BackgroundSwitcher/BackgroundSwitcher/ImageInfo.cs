using System;
using System.Drawing;
using JSON;

namespace BackgroundSwitcher {
    public class ImageInfo {
        public ImageInfo(string path, Size size) {
            Path = path;
            Size = size;
        }
        public int Area => Size.Width * Size.Height;
        public Bitmap Bitmap { get; set; }
        public Rectangle DestRect { get; set; }
        public DateTime LastModTimestamp { get; set; } = DateTime.MinValue;
        public DateTime LastShown { get; set; } = DateTime.MinValue;
        public string Path { get; }
        public double Ratio => (double) Size.Height / Size.Width;
        public Rectangle RectShown { get; set; }
        public bool Shown { get; set; } = false;
        public Size Size { get; }
        public bool Validated { get; set; }
        public JSONObject ToJSONObject() => new JSONObject().put("Path", Path).put("Width", Size.Width).put("Height", Size.Height).put("LastWrite", LastModTimestamp.Ticks).put("LastShown", LastShown.Ticks);
        public static ImageInfo FromJSONObject(JSONObject json) {
            var imageinfo =  new ImageInfo(json.getString("Path"), new Size(json.getInt("Width"), json.getInt("Height")));
            var s = json.optString("LastShown", DateTime.MinValue.Ticks.ToString());
            if (long.TryParse(s, out long l)) imageinfo.LastShown = new DateTime(l);
            s = json.optString("LastWrite", DateTime.MinValue.Ticks.ToString());
            if (long.TryParse(s, out l)) imageinfo.LastModTimestamp = new DateTime(l);
            return imageinfo;
        }
    }
}