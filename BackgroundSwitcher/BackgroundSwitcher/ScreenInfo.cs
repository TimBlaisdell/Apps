using System.Drawing;
using System.Windows.Forms;

namespace BackgroundSwitcher {
    public class ScreenInfo {
        public ScreenInfo(Screen screen) {
            _screen = screen;
            Bitmap = new Bitmap(_screen.Bounds.Width, _screen.Bounds.Height);
            InitBitmap();
        }
        public int Area => Size.Width * Size.Height;
        public Bitmap Bitmap { get; }
        public Rectangle Bounds => _screen.Bounds;
        public Size Size => _screen.Bounds.Size;
        public void InitBitmap() {
            using (var gfx = Graphics.FromImage(Bitmap)) {
                gfx.FillRectangle(Brushes.Black, 0, 0, _screen.Bounds.Width, _screen.Bounds.Height);
            }
        }
        private readonly Screen _screen;
    }
}