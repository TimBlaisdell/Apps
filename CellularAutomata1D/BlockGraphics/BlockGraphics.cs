using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace BG {
    public class BlockGraphics : PictureBox {
        // Constructors
        public BlockGraphics() : this(new Rectangle(-1000, -1000, 2000, 2000)) { }
        public BlockGraphics(Rectangle area) {
            SuspendRefreshes = false;
            _area = area;
            Controls.Add(_hscrollbar);
            Controls.Add(_vscrollbar);
            Controls.Add(_spacer);
            _hscrollbar.Visible = false;
            _vscrollbar.Visible = false;
            _hscrollbar.Minimum = 0;
            _hscrollbar.Maximum = area.Width;
            _vscrollbar.Minimum = 0;
            _vscrollbar.Maximum = area.Height;
            _hscrollbar.SmallChange = _vscrollbar.SmallChange = 1;
            _hscrollbar.LargeChange = _vscrollbar.LargeChange = 1;
            _hscrollbar.Scroll += _hscrollbar_Scroll;
            _vscrollbar.Scroll += _vscrollbar_Scroll;
            _spacer.Visible = false;
            PositionScrollBars();
        }
        public Rectangle Area {
            get { return _area; }
        }
        private bool HScrollBarNeeded {
            get { return _viewarea.Width > 0 && _viewarea.Width < _area.Width; }
        }
        public int MaxX {
            get { return _area.Right - 1; }
        }
        public int MaxY {
            get { return _area.Bottom - 1; }
        }
        public int MinX {
            get { return _area.Left; }
        }
        public int MinY {
            get { return _area.Top; }
        }
        public ModeEnum Mode { get; set; } = ModeEnum.Zoom;
        public bool SuspendRefreshes { get; set; }
        public Rectangle ViewArea {
            get { return _viewarea; }
            set {
                _viewarea = value;
                if (_viewarea.Width <= 0 || _viewarea.Height <= 0) return;
                Bitmap bmp = new Bitmap(_viewarea.Width, _viewarea.Height);
                Image = Image.FromHbitmap(bmp.GetHbitmap());
                Graphics gfx = Graphics.FromImage(Image);
                gfx.FillRectangle(new SolidBrush(Color.White), 0, 0, _viewarea.Width, _viewarea.Height);
                _hscrollbar.Maximum = _area.Width - _viewarea.Width;
                _vscrollbar.Maximum = _area.Height - _viewarea.Height;
                PositionScrollBars();
                Refresh();
            }
        }
        private bool VScrollBarNeeded {
            get { return _viewarea.Height > 0 && _viewarea.Height < _area.Height; }
        }
        private void _hscrollbar_Scroll(object sender, ScrollEventArgs e) {
            _viewarea.X = e.NewValue + _area.Left;
            Refresh();
        }
        private void _vscrollbar_Scroll(object sender, ScrollEventArgs e) {
            _viewarea.Y = e.NewValue + _area.Top;
            Refresh();
        }
        public void Clear(int x, int y) {
            Set(x, y, Color.White);
        }
        public Color Get(int x, int y) {
            Point normPt = new Point(x - _area.Left, y - _area.Top);
            if (normPt.X < 0 || normPt.X >= _area.Size.Width ||
                normPt.Y < 0 || normPt.Y >= _area.Size.Height) {
                throw new IndexOutOfRangeException("BlockGraphics.Get was called with out-of-range point (" + x + "," +
                                                   y + ")");
            }
            ulong index = (ulong)normPt.Y * (ulong)_area.Size.Width + (ulong)normPt.X;
            if (_blocks.Keys.Contains(index)) {
                return _blocks[index].Color;
            }
            return Color.White;
        }
        public void LoadBitmap(string filepath) {
            Image newImage;
            try {
                newImage = Image.FromFile(filepath);
            }
            catch {
                return;
            }
            Reset(new Rectangle(0, 0, newImage.Width, newImage.Height), new Rectangle(0, 0, newImage.Width, newImage.Height));
            Bitmap bmp = new Bitmap(newImage);
            for (int y = 0; y < bmp.Height; ++y) {
                for (int x = 0; x < bmp.Width; ++x) {
                    Color c = bmp.GetPixel(x, y);
                    if (c != Color.White) Set(x, y, c);
                }
            }
            Refresh();
        }
        public override void Refresh() {
            Draw();
            base.Refresh();
        }
        public void Reset(Rectangle area, Rectangle viewarea) {
            _area = area;
            _blocks = new SortedList<ulong, Block>();
            ViewArea = viewarea;
        }
        public void SaveBitmap(string filepath) {
            Image.Save(filepath);
        }
        public void Scroll(Point p) {
            Rectangle newViewArea = new Rectangle(_viewarea.X + p.X,
                                                  _viewarea.Y + p.Y,
                                                  _viewarea.Width,
                                                  _viewarea.Height);
            if (Rectangle.Intersect(newViewArea, _area) == newViewArea) {
                _viewarea = newViewArea;
                PositionScrollBars();
                if (!SuspendRefreshes) Refresh();
            }
        }
        public void Set(int x, int y) {
            Set(x, y, Color.Black);
        }
        public void Set(int x, int y, Color c) {
            Point normPt = new Point(x - _area.Left, y - _area.Top);
            if (normPt.X < 0 || normPt.X >= _area.Size.Width ||
                normPt.Y < 0 || normPt.Y >= _area.Size.Height) {
                throw new IndexOutOfRangeException("BlockGraphics.Set was called with out-of-range point (" + x + "," +
                                                   y + ")");
            }
            ulong index = (ulong)normPt.Y * (ulong)_area.Size.Width + (ulong)normPt.X;
            Block b;
            if (_blocks.Keys.Contains(index)) {
                b = _blocks[index];
            }
            else {
                b = new Block(x, y);
                _blocks.Add(index, b);
            }
            b.Color = c;
            if (_viewarea.Contains(x, y) && !SuspendRefreshes) Refresh();
        }
        public string ToJSON() {
            string s = "[";
            string comma = "";
            foreach (var b in _blocks.Values) {
                s += comma + b.ToJSON();
                comma = ", ";
            }
            return s;
        }
        public string ToXml() {
            return _blocks.Values.Aggregate("<blocks>", (current, b) => current + b.ToXml()) + "</blocks>";
        }
        protected void Draw() {
            if (Image == null) return;
            Graphics gfx = Graphics.FromImage(Image);
            gfx.Clear(Color.White);
            if (_viewarea.Size.Width == 0 || _viewarea.Size.Height == 0) return;
            for (int y = 0; y < _viewarea.Height; ++y) {
                for (int x = 0; x < _viewarea.Width; ++x) {
                    Block b;
                    Get(x + _viewarea.Left, y + _viewarea.Top, out b);
                    if (b != null) {
                        gfx.FillRectangle(new SolidBrush(b.Color), x, y, 1, 1);
                    }
                }
            }
        }
        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);
            switch (Mode) {
                case ModeEnum.Zoom:
                    switch (e.Button) {
                        case MouseButtons.Left:
                            if (_viewarea.Width > 10 && _viewarea.Height > 10) {
                                ViewArea = new Rectangle(_viewarea.X + 1,
                                                         _viewarea.Y + 1,
                                                         _viewarea.Width - 2,
                                                         _viewarea.Height - 2);
                            }
                            break;
                        case MouseButtons.Right:
                            Rectangle newViewArea = _viewarea;
                            if (newViewArea.X > _area.X) {
                                --newViewArea.X;
                                newViewArea.Width += 2;
                                while (newViewArea.Right > _area.Right) --newViewArea.Width;
                            }
                            if (newViewArea.Y > _area.Y) {
                                --newViewArea.Y;
                                newViewArea.Height += 2;
                                while (newViewArea.Bottom > _area.Bottom) --newViewArea.Height;
                            }
                            ViewArea = newViewArea;
                            break;
                    }
                    break;
            }
            Refresh();
        }
        protected override void OnPaint(PaintEventArgs pe) {
            pe.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            base.OnPaint(pe);
            Draw();
        }
        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            PositionScrollBars();
        }
        private void Get(int x, int y, out Block b) {
            b = null;
            Point normPt = new Point(x - _area.Left, y - _area.Top);
            if (normPt.X < 0 || normPt.X >= _area.Size.Width ||
                normPt.Y < 0 || normPt.Y >= _area.Size.Height) {
                throw new IndexOutOfRangeException("BlockGraphics.Get was called with out-of-range point (" + x + "," +
                                                   y + ")");
            }
            ulong index = (ulong)normPt.Y * (ulong)_area.Size.Width + (ulong)normPt.X;
            if (_blocks.Keys.Contains(index)) {
                b = _blocks[index];
            }
        }
        private void PositionScrollBars() {
            if (HScrollBarNeeded) {
                _hscrollbar.Top = Height - _hscrollbar.Height;
                _hscrollbar.Width = Width - _vscrollbar.Width;
                _hscrollbar.Visible = true;
                _hscrollbar.BringToFront();
                _spacer.Top = _hscrollbar.Top;
                _spacer.Left = _hscrollbar.Right;
                _spacer.Height = _hscrollbar.Height;
                _spacer.Width = _vscrollbar.Width;
                _hscrollbar.Value = _viewarea.Left - _area.Left;
            }
            else {
                _hscrollbar.Visible = false;
            }
            if (VScrollBarNeeded) {
                _vscrollbar.Left = Width - _vscrollbar.Width;
                _vscrollbar.Height = Height - _hscrollbar.Height;
                _vscrollbar.Visible = true;
                _vscrollbar.BringToFront();
                _spacer.Top = _vscrollbar.Bottom;
                _spacer.Left = _vscrollbar.Left;
                _spacer.Height = _hscrollbar.Height;
                _spacer.Width = _vscrollbar.Width;
                _vscrollbar.Value = _viewarea.Top - _area.Top;
            }
            else {
                _vscrollbar.Visible = false;
            }
            if (HScrollBarNeeded || VScrollBarNeeded) {
                _spacer.Visible = true;
                _spacer.BringToFront();
            }
            else {
                _spacer.Visible = false;
            }
        }
        private Rectangle _area; // always set in the constructor
        /// <summary>
        ///     These are all the blocks we know about. The index is (Y * _maxPt.X) + X
        /// </summary>
        private SortedList<ulong, Block> _blocks = new SortedList<ulong, Block>();
        private readonly ScrollBar _hscrollbar = new HScrollBar();
        private readonly Panel _spacer = new Panel();
        private Rectangle _viewarea = new Rectangle(0, 0, 0, 0);
        private readonly ScrollBar _vscrollbar = new VScrollBar();
        public enum ModeEnum {
            None,
            Edit,
            Zoom
        };
    }
    internal class Block {
        public Block(int x, int y) {
            X = x;
            Y = y;
            Color = Color.White;
        }
        public string ToJSON() {
            // {"X": 0, "Y": 0}
            return "{\"X\": " + X + ", \"Y\": " + Y + "}";
        }
        public string ToXml() {
            return "<block x='" + X + "' y='" + Y + "' color='" + Color + "'/>";
        }
        public Color Color;
        public int X;
        public int Y;
    }
}