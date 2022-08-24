using System;
using System.Drawing;
using System.Windows.Forms;
using JSON;

namespace BackgroundSwitcher {
    public partial class ImagePanel : UserControl {
        public ImagePanel() {
            InitializeComponent();
        }
        public override Image BackgroundImage {
            get => base.BackgroundImage;
            set {
                base.BackgroundImage = value;
                _drawingMode = DrawingMode.None;
                _curRect = Rectangle.Empty;
                SelectedRectChanged?.Invoke(this, EventArgs.Empty);
                Invalidate();
            }
        }
        public Rectangle SelectedRect {
            get => _curRect.IsEmpty || _curRect.Height == 0 || _curRect.Width == 0 ? Rectangle.Empty : _curRect;
            set {
                if (_curRect == value) return;
                _curRect = value;
                SelectedRectChanged?.Invoke(this, EventArgs.Empty);
                Invalidate();
            }
        }
        public event EventHandler SelectedRectChanged;
        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            _mousepos = e.Location;
            if (_curRect.IsEmpty) {
                _curRect = new Rectangle(_mousepos.X, _mousepos.Y, 1, 1);
                _drawingMode = DrawingMode.FullReect;
                _anchor = e.Location;
                SelectedRectChanged?.Invoke(this, EventArgs.Empty);
            }
            else {
                var ld = Math.Abs(_mousepos.X - _curRect.Left);
                var rd = Math.Abs(_mousepos.X - _curRect.Right);
                var td = Math.Abs(_mousepos.Y - _curRect.Top);
                var bd = Math.Abs(_mousepos.Y - _curRect.Bottom);
                if (ld <= 2 || rd <= 2) {
                    if (td > 2 && bd > 2) {
                        _drawingMode = DrawingMode.LeftRight;
                        _anchor = ld <= 2 ? new Point(_curRect.Right, _curRect.Top) : new Point(_curRect.Left, _curRect.Top);
                    }
                    else {
                        _drawingMode = DrawingMode.FullReect;
                        _anchor = new Point(ld <= 2 ? _curRect.Right : _curRect.Left, td <= 2 ? _curRect.Bottom : _curRect.Top);
                    }
                }
                else if (td <= 2 || bd <= 2) {
                    _drawingMode = DrawingMode.TopBottom;
                    _anchor = td < 2 ? new Point(_curRect.Left, _curRect.Bottom) : new Point(_curRect.Left, _curRect.Top);
                }
                else {
                    _curRect = new Rectangle(_mousepos.X, _mousepos.Y, 1, 1);
                    _drawingMode = DrawingMode.FullReect;
                    _anchor = e.Location;
                    SelectedRectChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            Invalidate(); //new Rectangle(Math.Min(_mousepos.X, e.Location.X) - 1, Math.Min(_mousepos.Y, e.Location.Y) - 1, Math.Abs(_mousepos.X - e.Location.X) + 2, Math.Abs(_mousepos.Y - e.Location.Y) + 2));
            bool symetric = (ModifierKeys & Keys.Control) != 0;
            var poschange = new Point(e.Location.X - _mousepos.X, e.Location.Y - _mousepos.Y);
            _mousepos = e.Location;
            if (_drawingMode != DrawingMode.None) {
                switch (_drawingMode) {
                    case DrawingMode.FullReect:
                        if (symetric) _anchor = new Point(_anchor.X - poschange.X, _anchor.Y - poschange.Y);
                        _curRect = new Rectangle(Math.Min(_mousepos.X, _anchor.X), Math.Min(_mousepos.Y, _anchor.Y), Math.Abs(_mousepos.X - _anchor.X), Math.Abs(_mousepos.Y - _anchor.Y));
                        break;
                    case DrawingMode.LeftRight:
                        if (symetric) _anchor = new Point(_anchor.X - poschange.X, _anchor.Y);
                        _curRect = new Rectangle(Math.Min(_mousepos.X, _anchor.X), _curRect.Y, Math.Abs(_mousepos.X - _anchor.X), _curRect.Height);
                        break;
                    case DrawingMode.TopBottom:
                        if (symetric) _anchor = new Point(_anchor.X, _anchor.Y - poschange.Y);
                        _curRect = new Rectangle(_curRect.X, Math.Min(_mousepos.Y, _anchor.Y), _curRect.Width, Math.Abs(_mousepos.Y - _anchor.Y));
                        break;
                }
                SelectedRectChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);
            _drawingMode = DrawingMode.None;
            if (_curRect.Width < 3 || _curRect.Height < 3) {
                _curRect = Rectangle.Empty;
                SelectedRectChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e) {
            if (BackgroundImage == null) {
                base.OnPaintBackground(e);
                return;
            }
            if (Width > BackgroundImage.Width) e.Graphics.FillRectangle(Brushes.LightGray, BackgroundImage.Width, 0, Width - BackgroundImage.Width, Height);
            if (Height > BackgroundImage.Height) e.Graphics.FillRectangle(Brushes.LightGray, 0, BackgroundImage.Height, Width, Height - BackgroundImage.Height);
            e.Graphics.DrawImage(BackgroundImage, 0, 0);
            var pen = new Pen(Color.Black, 1) {DashPattern = new float[] {5, 5}};
            e.Graphics.DrawLine(pen, new Point(_mousepos.X, 0), new Point(_mousepos.X, Height));
            e.Graphics.DrawLine(pen, new Point(0, _mousepos.Y), new Point(Width, _mousepos.Y));
            pen = new Pen(Color.White, 1) {DashPattern = new float[] {5, 5}, DashOffset = 5};
            e.Graphics.DrawLine(pen, new Point(_mousepos.X, 0), new Point(_mousepos.X, Height));
            e.Graphics.DrawLine(pen, new Point(0, _mousepos.Y), new Point(Width, _mousepos.Y));
            if (!_curRect.IsEmpty) {
                e.Graphics.DrawRectangle(Pens.Brown, _curRect.X, _curRect.Y, _curRect.Width, _curRect.Height);
                pen = new Pen(Color.Yellow, 1) {DashPattern = new float[] {4, 4}};
                e.Graphics.DrawRectangle(pen, _curRect.X, _curRect.Y, _curRect.Width, _curRect.Height);
            }
        }
        private Point _anchor = new Point(0, 0);
        private Rectangle _curRect;
        private DrawingMode _drawingMode = DrawingMode.None;
        private Point _mousepos = new Point(0, 0);
        private enum DrawingMode {
            None,
            FullReect,
            LeftRight,
            TopBottom
        }
    }
}