using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ChaosGame {
    public class MyPBox : PictureBox {
        public MyPBox() : base() {
        }
        protected override void OnPaintBackground(PaintEventArgs pevent) {
            pevent.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            base.OnPaintBackground(pevent);
        }
    }
}