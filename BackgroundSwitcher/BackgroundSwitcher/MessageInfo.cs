using System.Drawing;

namespace BackgroundSwitcher.Panels {
    public struct MessageInfo {
        public MessageInfo(Color c, string msg) {
            Color = c;
            Message = msg;
        }
        public Color Color;
        public string Message;
    }
}