using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using JSON;

namespace BackgroundSwitcher {
    public sealed class JSONImageInfo : JSONObject {
        public JSONImageInfo() {
        }
        //public JSONImageInfo(string path, Size size, RectangleF? focusrect = null) {
        //    Path = path;
        //    Size = size;
        //    if (focusrect != null) {
        //        FocusRect = focusrect.Value;
        //    }
        //}
        public JSONImageInfo(JSONImageInfo orig) : this() {
            if (orig == null) return;
            foreach (string name in orig.Names) {
                put(name, orig.getValue(name));
            }
        }
        //public int Area => Size.Width * Size.Height;
        public Bitmap Bitmap { get; set; }
        public double DestRatio => DestRect.Width == 0 ? 0 : (double)DestRect.Height / DestRect.Width;
        public Rectangle DestRect {
            get {
                if (has("DestRect")) {
                    var obj = getJSONObject("DestRect");
                    return new Rectangle(obj.getInt("X"), obj.getInt("Y"), obj.getInt("Width"), obj.getInt("Height"));
                }
                return Rectangle.Empty;
            }
            set => put("DestRect", new JSONObject().put("X", value.X).put("Y", value.Y).put("Width", value.Width).put("Height", value.Height));
        }
        public RectangleF FocusRect {
            get {
                if (has("FocusRect") && getJSONObject("FocusRect").optInt("Width") > 0 && getJSONObject("FocusRect").optInt("Height") > 0) {
                    var obj = getJSONObject("FocusRect");
                    return new RectangleF(obj.optInt("X"), obj.optInt("Y"), obj.optInt("Width"), obj.optInt("Height"));
                }
                var size = Size;
                var difsize = new SizeF((float)(size.Width * MinRatioDiff), (float)(size.Height * MinRatioDiff));
                return new RectangleF(difsize.Width / 2, difsize.Height / 2, size.Width - difsize.Width, size.Height - difsize.Height);
            }
            //private set {
            //    var rect = RectangleF.Intersect(new RectangleF(0, 0, Size.Width, Size.Height), value);
            //    var obj = new JSONObject();
            //    obj.put("X", rect.X);
            //    obj.put("Y", rect.Y);
            //    obj.put("Width", rect.Width);
            //    obj.put("Height", rect.Height);
            //    put("FocusRect", obj);
            //}
        }
        public ulong HashCode {
            get => optULong("Hash");
            set => put("Hash", value);
        }
        public DateTime LastShown {
            get => !has("RecentShows") && has("LastShown")
                ? long.TryParse(getString("LastShown"), out long l) ? new DateTime(l) : DateTime.TryParse(getString("LastShown"), out DateTime dt) ? dt : DateTime.MinValue
                : RecentShows.LastOrDefault();
            set {
                JSONArray json;
                if (!has("RecentShows") && has("LastShown")) {
                    json = new JSONArray().put(LastShown.ToString());
                    remove("LastShown");
                }
                else json = optJSONArray("RecentShows") ?? new JSONArray();
                string s = value.ToString();
                if (s == (string)json.LastOrDefault()) return;
                json.put(s);
                while (json.Count > 4) json.RemoveAt(0);
                put("RecentShows", json);
            }
        }
        public long LastShownTicks => LastShown.Ticks;
        public DateTime LastWrite {
            get => new DateTime(LastWriteTicks);
            set => put("LastWrite", value.ToString("O"));
        }
        public long LastWriteTicks => long.TryParse(optString("LastWrite", DateTime.MinValue.Ticks.ToString()), out long l) ? l :
            DateTime.TryParse(optString("LastWrite", "xxx"), out DateTime dt) ? dt.Ticks : DateTime.MinValue.Ticks;
        public double MaxRatio => Size.Height / FocusRect.Width;
        public double MinRatio => FocusRect.Height / Size.Width;
        //public double MinRatio {
        //    get {
        //        //var sz = FocusRect.Size;
        //        //var r = (double) sz.Height / sz.Width;
        //        //return r > Ratio ? Ratio : r;
        //        return FocusRect.Height / Size.Width;
        //    }
        //}
        public static double MinRatioDiff { private get; set; } = 0.1;
        public string Path {
            get => optString("Path");
            set => put("Path", value);
        }
        public double Ratio => (double)Size.Height / Size.Width;
        public List<DateTime> RecentShows {
            get {
                var a = new List<DateTime>();
                if (!has("RecentShows") && has("LastShown")) {
                    a.Add(LastShown);
                }
                else {
                    var json = optJSONArray("RecentShows") ?? new JSONArray();
                    foreach (string s in json) a.Add(DateTime.Parse(s));
                }
                return a;
            }
        }
        public Rectangle RectShown { get; set; }
        public int ShowCount {
            get => optInt("ShowCount");
            set => put("ShowCount", value);
        }
        public bool Shown { get; set; } = false;
        public Size Size {
            get => new Size(optInt("Width"), optInt("Height"));
            set {
                put("Width", value.Width);
                put("Height", value.Height);
            }
        }
        public bool Validated { get; set; }
        //private Rectangle _destRect;
    }
}