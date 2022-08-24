using System.Drawing;
using JSON;

namespace ChaosGame {
    public class PointInfo {
        public PointInfo(PointF p, Color c) {
            P = p;
            C = c;
        }
        public JSONObject toJSON() => new JSONObject().put("X", P.X).put("Y", P.Y).put("R", C.R).put("G", C.G).put("B", C.B);
        public override string ToString() => P + " " + C;
        public static PointInfo FromJSON(JSONObject obj) => new PointInfo(new PointF((float)obj.optDouble("X", 0), (float)obj.optDouble("Y", 0)), Color.FromArgb(obj.optInt("R"), obj.optInt("G"), obj.optInt("B")));
        public Color C;
        public PointF P;
    }
}