using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SingleLineDrawing {
    public partial class LineDrawer : UserControl {
        public LineDrawer() {
            InitializeComponent();
        }
        public bool CreateRotatingSequence { get; set; }
        public int RotatingSequenceAngleIncrement { get; set; }
        public string RotatingSequenceFolder { get; set; }
        public bool Running { get; private set; }
        /// <summary>
        ///     distance between arcs in spiral will be magnitude * (2 * pi)
        /// </summary>
        public int SpiralDistanceMagnitude { get; set; }
        public Image TargetImage {
            get => _targetImage;
            set {
                if (Running) throw new Exception("Cannot set target image while running.");
                _targetImage = value == null ? null : MakeGrayscale(value);
                if (_targetImage != null) {
                    _size = _targetImage.Size;
                    InitializeCanvas();
                }
                else _size = Size.Empty;
                Invalidate();
            }
        }
        private void timer_Tick(object sender, EventArgs e) {
            if (Running && (_threadActive || _drawOnceMore) && _invalidRect != Rectangle.Empty) {
                double hscale = (double) Size.Width / _size.Width;
                double vscale = (double) Size.Height / _size.Height;
                var invrect = new Rectangle((int) Math.Floor(_invalidRect.X * hscale),
                                            (int) Math.Floor(_invalidRect.Y * vscale),
                                            (int) Math.Ceiling(_invalidRect.Width * hscale),
                                            (int) Math.Ceiling(_invalidRect.Height * vscale));
                Invalidate(invrect);
                lock (_lockobj) _invalidRect = Rectangle.Empty;
                _drawOnceMore = false;
            }
        }
        public Image GetImage() {
            if (!Running) return _targetImage;
            var bmp = new Bitmap(_size.Width, _size.Height);
            using (var gfx = Graphics.FromImage(bmp)) {
                lock (_lockobj) {
                    gfx.DrawImage(_thickCanvas, new Rectangle(0, 0, _size.Width, _size.Height), new Rectangle(0, 0, _size.Width, _size.Height), GraphicsUnit.Pixel);
                    gfx.DrawImage(_thinCanvas, new Rectangle(0, 0, _size.Width, _size.Height), new Rectangle(0, 0, _size.Width, _size.Height), GraphicsUnit.Pixel);
                }
            }
            return bmp;
        }
        public void Start() {
            if (Running || _targetImage == null) return;
            Running = true;
            timer.Start();
            new Thread(ProcessingThread).Start();
        }
        public void Stop() {
            if (!Running) return;
            Running = false;
            timer.Stop();
            while (_threadActive) Thread.Sleep(10);
        }
        protected override void OnPaint(PaintEventArgs e) {
            if (!Running) {
                if (_targetImage == null) e.Graphics.FillRectangle(Brushes.AliceBlue, 0, 0, Size.Width, Size.Height);
                else e.Graphics.DrawImage(_targetImage, new Rectangle(0, 0, Size.Width, Size.Height), new Rectangle(0, 0, _targetImage.Width, _targetImage.Height), GraphicsUnit.Pixel);
            }
            else {
                lock (_lockobj) {
                    e.Graphics.DrawImage(_thickCanvas, new Rectangle(0, 0, Size.Width, Size.Height), new Rectangle(0, 0, _size.Width, _size.Height), GraphicsUnit.Pixel);
                    e.Graphics.DrawImage(_thinCanvas, new Rectangle(0, 0, Size.Width, Size.Height), new Rectangle(0, 0, _size.Width, _size.Height), GraphicsUnit.Pixel);
                }
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e) {
            //base.OnPaintBackground(e);
        }
        protected override void OnSizeChanged(EventArgs e) {
            base.OnSizeChanged(e);
            Invalidate();
        }
        private void CreateSpiralOnCanvas() {
            InitializeCanvas();
            // Step 1 - draw the initial line.
            _points.Clear();
            Point center = new Point(_targetImage.Width / 2, _targetImage.Height / 2);
            var points = GetSpiralPoints(center, SpiralDistanceMagnitude, _currentAngle * ((float) Math.PI / 180), Math.Min(center.DistanceTo(new Point(0, center.Y)), center.DistanceTo(new Point(center.X, 0))));
            lock (_lockobj) {
                using (var gfx = Graphics.FromImage(_thinCanvas)) {
                    gfx.DrawLines(Pens.Black, points.ToArray());
                }
                var data = _thinCanvas.Lock();
                _thinCanvas.IterateOver(data,
                                        (x, y, a, r, g, b, index) => {
                                            if (a != 0) _points.Add(new Point(x, y));
                                            return null;
                                        });
                _thinCanvas.Unlock(data);
                InvalidateRect(new Rectangle(0, 0, _size.Width, _size.Height));
            }
            // Step 2, iterate over all the points and draw an appropriately sized circle onto _thickCanvas based on the darkness at that point.
            int rectsize = (int) Math.Truncate(SpiralDistanceMagnitude * 2 * Math.PI - 1);
            var idata = ((Bitmap) _targetImage).Lock();
            foreach (var p in _points) {
                if (!Running) break;
                var rect = new Rectangle(p.X - rectsize / 2, p.Y - rectsize / 2, rectsize, rectsize);
                int total = 0;
                int count = 0;
                ((Bitmap) _targetImage).IterateOver(idata,
                                                    rect,
                                                    (x, y, a, r, g, b, index) => {
                                                        total += r;
                                                        ++count;
                                                        return null;
                                                    });
                float avg = total / (float) count;
                float ratio = 1 - avg / 255;
                if (ratio < 0 || ratio > 1) throw new Exception("Should be impossible.");
                float diameter = rectsize * ratio;
                if (diameter < 1) continue;
                lock (_lockobj) {
                    var circleRect = new RectangleF(p.X - diameter / 2, p.Y - diameter / 2, diameter, diameter);
                    using (var gfx = Graphics.FromImage(_thickCanvas)) {
                        gfx.FillEllipse(Brushes.Black, circleRect);
                    }
                    InvalidateRect(circleRect);
                }
                Thread.Sleep(0);
            }
            ((Bitmap) _targetImage).Unlock(idata);
        }
        // Return points that define a spiral.
        private List<PointF> GetSpiralPoints(PointF center, float A, float angle_offset, double max_r) {
            // Get the points.
            List<PointF> points = new List<PointF>();
            const float dtheta = (float) (5 * Math.PI / 180); // Five degrees.
            for (float theta = 0;; theta += dtheta) {
                // Calculate r.
                float r = A * theta;
                // Convert to Cartesian coordinates.
                float x, y;
                PolarToCartesian(r, theta + angle_offset, out x, out y);
                // Center.
                x += center.X;
                y += center.Y;
                // Create the point.
                points.Add(new PointF((float) x, (float) y));
                // If we have gone far enough, stop.
                if (r > max_r) break;
            }
            return points;
        }
        private void InitializeCanvas() {
            lock (_lockobj) {
                var bmp = new Bitmap(_targetImage.Width, _targetImage.Height);
                var data = bmp.Lock();
                bmp.IterateOver(data, (x, y, a, r, g, b, index) => new[] {Color.White.A, Color.White.R, Color.White.G, Color.White.B});
                bmp.Unlock(data);
                _thickCanvas = bmp;
                bmp = new Bitmap(_targetImage.Width, _targetImage.Height);
                data = bmp.Lock();
                bmp.IterateOver(data, (x, y, a, r, g, b, index) => new[] {(byte) 0, Color.White.R, Color.White.G, Color.White.B});
                bmp.Unlock(data);
                _thinCanvas = bmp;
            }
        }
        private void InvalidateRect(RectangleF imgrect) {
            if (imgrect == RectangleF.Empty) return;
            var boundingrect = imgrect.BoundingRect();
            lock (_lockobj) _invalidRect = _invalidRect == Rectangle.Empty ? boundingrect : Rectangle.Union(_invalidRect, boundingrect);
        }
        // Convert polar coordinates into Cartesian coordinates.
        private void PolarToCartesian(float r, float theta, out float x, out float y) {
            x = (float) (r * Math.Cos(theta));
            y = (float) (r * Math.Sin(theta));
        }
        private void ProcessingThread() {
            _threadActive = true;
            try {
                if (CreateRotatingSequence) {
                    for (_currentAngle = 0; _currentAngle < 360; _currentAngle += Math.Max(RotatingSequenceAngleIncrement, 1)) {
                        if (!Running) return;
                        CreateSpiralOnCanvas();
                        Thread.Sleep(100);
                        GetImage().Save(Path.Combine(RotatingSequenceFolder, "frame" + _currentAngle + ".png"));
                    }
                }
                else CreateSpiralOnCanvas();
            }
            finally {
                _threadActive = false;
                _drawOnceMore = true;
            }
        }
        //private bool ValidPos(Point p) => p.X >= 0 && p.Y >= 0 && p.X < _size.Width && p.Y < _size.Height;
        private static Image MakeGrayscale(Image original) {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            //get a graphics object from the new image
            using (Graphics g = Graphics.FromImage(newBitmap)) {
                //create the grayscale ColorMatrix
                ColorMatrix colorMatrix = new ColorMatrix(
                    new[] {
                              new[] {.3f, .3f, .3f, 0, 0},
                              new[] {.59f, .59f, .59f, 0, 0},
                              new[] {.11f, .11f, .11f, 0, 0},
                              new[] {0f, 0f, 0f, 1f, 0f},
                              new[] {0f, 0f, 0f, 0f, 1f}
                          });
                //create some image attributes
                ImageAttributes attributes = new ImageAttributes();
                //set the color matrix attribute
                attributes.SetColorMatrix(colorMatrix);
                //draw the original image on the new image
                //using the grayscale color matrix
                g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            }
            return newBitmap;
        }
        private float _currentAngle = 0;
        //private float _bigRadiusIncrement = 1F;
        private bool _drawOnceMore;
        private Rectangle _invalidRect = Rectangle.Empty;
        //private float _littleRadiusIncrement = 0.1F;
        private readonly object _lockobj = new object();
        private readonly List<Point> _points = new List<Point>();
        private Size _size = Size.Empty;
        //private float _startingRadius = 1;
        private Image _targetImage;
        private Image _thickCanvas;
        private Bitmap _thinCanvas;
        private bool _threadActive;
    }
}