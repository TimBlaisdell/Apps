using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using JSON;
using PointGame.Properties;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace ChaosGame {
    public class PointGamePanel : UserControl {
        public PointGamePanel() {
            InitializeComponent();
            menuMakePointsSameColor.Image = new Bitmap(Resources.dummy_color);
            using (var gfx = Graphics.FromImage(menuMakePointsSameColor.Image)) {
                gfx.FillRectangle(Brushes.Black, 0, 0, menuMakePointsSameColor.Image.Width, menuMakePointsSameColor.Image.Height);
            }
            TravelDist = 0.5F;
        }
        public override Image BackgroundImage {
            get => base.BackgroundImage;
            set {
                base.BackgroundImage = value;
                Bmp = (Bitmap)value;
            }
        }
        // ReSharper disable InconsistentlySynchronizedField
        public Bitmap Bmp {
            get => _bmp;
            set {
                if (value == null || value.Width == 0 || value.Height == 0) {
                    _fieldRect = RectangleF.Empty;
                    return;
                }
                _fieldRect = new RectangleF(0, 0, value.Width, value.Height);
                _bmp = new Bitmap(value.Width, value.Height);
                //using (var gfx = Graphics.FromImage(_bmp)) {
                //    gfx.FillRectangle(new SolidBrush(Color.FromArgb(0, 255, 255, 255)), new Rectangle(0, 0, FieldSize.Width, FieldSize.Height));
                //}
                lock (_points) {
                    var clr = Color.FromArgb(0, 255, 255, 255);
                    _bmp.IterateOver((x, y, a, r, g, b, index, itres) => itres.Color = clr);
                    _points.Clear();
                    value.IterateOver((x, y, a, r, g, b, index, itres) => {
                                          var c = Color.FromArgb(r, g, b);
                                          if (!c.RGBEquals(Color.White)) {
                                              _points.Add(new PointInfo(new Point(x, y), c));
                                          }
                                      });
                    if (_points.Count > 0) PointsCreated?.Invoke(this, EventArgs.Empty);
                    CalcMinMaxDist();
                }
                _bgImage = new Bitmap(FieldSize.Width, FieldSize.Height);
                using (var gfx = Graphics.FromImage(_bgImage)) {
                    gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, FieldSize.Width, FieldSize.Height));
                }
                GenerateHilitesImage();
                _paintBackground = true;
                Invalidate();
            }
        }
        // ReSharper restore InconsistentlySynchronizedField
        public bool CircularPath { get; set; }
        public int ComboMix { get; set; } = -1;
        public float ConstantDist { get; set; }
        public int Count {
            get {
                lock (_points) return _points.Count;
            }
        }
        public DistanceMeaning DistMeaning { get; set; }
        public bool DrawLines { get; set; }
        public Size FieldSize => _fieldRect.Size.ToSize();
        public bool HilitePoints { get; set; }
        public bool IncludeCurPutInMoveCalc { get; set; }
        public ulong Iterations { get; private set; }
        public ulong MaxIterations { get; set; }
        public bool MixColors { get; set; }
        /// <summary>
        ///     Used with "move toward avg/sub of last N points".
        /// </summary>
        public int MoveN { get; set; }
        /// <summary>
        ///     Used with "pick among N nearest/farthest points".
        /// </summary>
        public int PickN { get; set; }
        public OnePointMovement PickOneMovement { get; set; }
        public OnePointSelection PickOneSelection { get; set; }
        public double PickRadius { get; set; }
        public TwoPointMovement PickTwoMovement { get; set; }
        public TwoPointSelection PickTwoSelection { get; set; }
        public PointSelection PtSelection { get; set; }
        public bool Running { get; set; }
        public PointInfo SelectedPoint { get; private set; }
        public float TravelDist { get; set; }
        public event EventHandler PointsCreated;
        public event EventHandler Stopped;
        private void cmenuPoint_Closed(object sender, ToolStripDropDownClosedEventArgs e) {
            _pointMenuOpen = false;
        }
        private void cmenuPoint_Opened(object sender, EventArgs e) {
            _pointMenuOpen = true;
        }
        private void menuDeletePoint_Click(object sender, EventArgs e) {
            if (SelectedPoint == null) return;
            lock (_points) {
                _points.Remove(SelectedPoint);
            }
            GenerateHilitesImage();
            Invalidate();
        }
        private void menuMakePointsSameColor_Click(object sender, EventArgs e) {
            lock (_points) {
                if (_points.Count == 0) return;
            }
            colorDialog.Color = SelectedPoint?.C ?? Color.Black;
            if (colorDialog.ShowDialog(this) == DialogResult.OK) {
                lock (_points) {
                    foreach (var p in _points) {
                        p.C = colorDialog.Color;
                    }
                }
                GenerateHilitesImage();
                if (HilitePoints) Invalidate();
            }
        }
        private void menuPointColor_Click(object sender, EventArgs e) {
            if (SelectedPoint == null) return;
            colorDialog.Color = SelectedPoint.C;
            if (colorDialog.ShowDialog(this) == DialogResult.OK) {
                SelectedPoint.C = colorDialog.Color;
                GenerateHilitesImage();
                if (HilitePoints) Invalidate();
            }
        }
        private void menuPointLocation_KeyPress(object sender, KeyPressEventArgs e) {
        }
        private void menuPointLocation_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                if (SelectedPoint == null) return;
                int iFirstNum = -1, lFirstNum = 0, iSecondNum = -1, lSecondNum = 0;
                string s = menuPointLocation.Text;
                for (int i = 0; i < menuPointLocation.Text.Length; ++i) {
                    char c = s[i];
                    if (iFirstNum < 0) {
                        if (char.IsDigit(c) || c == '.') iFirstNum = i;
                    }
                    else if (lFirstNum == 0) {
                        if (!char.IsDigit(c) && c != '.') lFirstNum = i - iFirstNum;
                    }
                    else if (iSecondNum < 0) {
                        if (char.IsDigit(c) || c == '.') iSecondNum = i;
                    }
                    else if (!char.IsDigit(c) && c != '.' || i == s.Length - 1) {
                        lSecondNum = (char.IsDigit(c) || c == '.' ? i + 1 : i) - iSecondNum;
                        break;
                    }
                }
                if (iFirstNum < 0 || iSecondNum < 0 || lFirstNum == 0 || lSecondNum == 0) {
                    MessageBox.Show("Incorrect format.", "Error");
                    return;
                }
                float x, y;
                try {
                    x = float.Parse(s.Substring(iFirstNum, lFirstNum));
                    y = float.Parse(s.Substring(iSecondNum, lSecondNum));
                }
                catch {
                    MessageBox.Show("Unable to parse.", "Error");
                    return;
                }
                SelectedPoint.P = new PointF(x, y);
                GenerateHilitesImage();
                Invalidate();
                e.Handled = true;
                cmenuPoint.Close();
            }
        }
        private void menuSave_Click(object sender, EventArgs e) {
            if (saveImageDialog.ShowDialog(this) == DialogResult.OK) {
                Bmp.Save(saveImageDialog.FileName);
            }
        }
        public void Clear() {
            //lock (_points) {
            //    _points.Clear();
            //}
            _fieldRect = new RectangleF(0, 0, FieldSize.Width, FieldSize.Height);
            _bmp = new Bitmap(FieldSize.Width, FieldSize.Height);
            using (var gfx = Graphics.FromImage(_bmp)) {
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, FieldSize.Width, FieldSize.Height));
            }
            _bgImage = new Bitmap(FieldSize.Width, FieldSize.Height);
            using (var gfx = Graphics.FromImage(_bgImage)) {
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, FieldSize.Width, FieldSize.Height));
            }
            GenerateHilitesImage();
            Iterations = 0;
            Invalidate();
        }
        //public void Draw() {
        //    if (_bmp == null) return;
        //    lock (_bmp) {
        //        for (int y = 0; y < FieldSize.Height; ++y) {
        //            for (int x = 0; x < FieldSize.Width; ++x) {
        //                if (_mixCounters[x, y].Changed) _bmp.SetPixel(x, y, _mixCounters[x, y].Color);
        //            }
        //        }
        //        //_bmp.IterateOver((x, y, a, r, g, b, index, itres) => {
        //        //                     if (_mixCounters[x, y].Changed) {
        //        //                         itres.Color = _mixCounters[x, y].Color;
        //        //                     }
        //        //                 });
        //    }
        //    Invalidate();
        //}
        public void FromJSON(JSONObject obj) {
            Clear();
            var parray = obj.getJSONArray("Points");
            lock (_points) {
                foreach (JSONObject p in parray) {
                    _points.Add(PointInfo.FromJSON(p));
                }
                if (_points.Count > 0) PointsCreated?.Invoke(this, EventArgs.Empty);
                CalcMinMaxDist();
            }
        }
        public void RandomizeBlackPoints() {
            int[] indexes = new int[_replacementColors.Length];
            for (int i = 0; i < indexes.Length; ++i) indexes[i] = i;
            for (int i = 0; i < indexes.Length; ++i) {
                int i2 = _rand.Next(_replacementColors.Length);
                if (i == i2) continue;
                int temp = indexes[i];
                indexes[i] = indexes[i2];
                indexes[i2] = temp;
            }
            lock (_points) {
                var blackpoints = _points.Where(p => p.C.RGBEquals(Color.Black));
                int i = 0;
                foreach (var p in blackpoints) {
                    p.C = _replacementColors[indexes[i]];
                    ++i;
                    if (i == indexes.Length) i = 0;
                }
            }
            GenerateHilitesImage();
        }
        public void ThreadProc() {
            try {
                Iterations = 0;
                _mixCounters = new MixInfo[FieldSize.Width, FieldSize.Height];
                //_mixCounters = new int[FieldSize.Width, FieldSize.Height];
                for (int y = 0; y < FieldSize.Height; ++y) {
                    for (int x = 0; x < FieldSize.Width; ++x) {
                        _mixCounters[x, y].Color = Color.White;
                        //_mixCounters[x, y].Changed = false;
                    }
                }
                PointInfo curpoint = null;
                _prevPoints.Clear();
                while (Running) {
                    lock (_bmp) {
                        for (int reps = 0; reps < 1000 && Running; ++reps) {
                            // pick new point(s)
                            var target = GetNextTarget(curpoint);
                            var oldCurPoint = curpoint;
                            // set the curpoint accordingly
                            curpoint = GetNewCurpoint(curpoint, target);
                            // plot the point.
                            int x = (int)Math.Round(curpoint.P.X);
                            int y = (int)Math.Round(curpoint.P.Y);
                            //// allow for rounding one pixel out of range.
                            //if (x == -1) ++x;
                            //if (y == -1) ++y;
                            //if (x == FieldSize.Width) --x;
                            //if (y == FieldSize.Height) --y;
                            //// if it went farther off the field, forget it.
                            //if (x < 0 || y < 0 || x >= FieldSize.Width || y >= FieldSize.Height) continue;
                            if (MixColors) {
                                Color c;
                                int count;
                                if (x >= 0 && y >= 0 && x < FieldSize.Width && y < FieldSize.Height) {
                                    //c = _bmp.GetPixel(x, y);
                                    c = _mixCounters[x, y].Color;
                                    count = _mixCounters[x, y].Count;
                                    ++_mixCounters[x, y].Count;
                                   
                                }
                                else {
                                    c = Color.White;
                                    count = 0;
                                }
                                if (DrawLines) {
                                    if (oldCurPoint != null && !oldCurPoint.P.EqualTo(curpoint.P)) { // don't draw a line the first time since there's no previous point to draw a line from.
                                        var newc = Color.FromArgb((c.R * count + curpoint.C.R) / (count + 1), (c.G * count + curpoint.C.G) / (count + 1), (c.B * count + curpoint.C.B) / (count + 1));
                                        using (var gfx = Graphics.FromImage(_bmp)) {
                                            var tempnewp = curpoint.P;
                                            if (PtSelection == PointSelection.PickTwo && PickTwoMovement == TwoPointMovement.Along3PointArc) {
                                                try {
                                                    using (var path = GetArcPath(oldCurPoint.P, target.tp1.P, target.tp2.P, TravelDist)) {
                                                        if (!path.PathPoints[path.PathPoints.Length - 1].EqualTo(curpoint.P)) throw new Exception("Insane!");
                                                        using (Brush aGradientBrush = new LinearGradientBrush(oldCurPoint.P, tempnewp, Color.FromArgb(10, oldCurPoint.C), Color.FromArgb(10, newc))) {
                                                            using (Pen aGradientPen = new Pen(aGradientBrush)) {
                                                                gfx.DrawPath(aGradientPen, path);
                                                            }
                                                        }
                                                    }
                                                }
                                                catch {
                                                    // do nothing.
                                                    int z = 0;
                                                }
                                            }
                                            else {
                                                var dist = tempnewp.X - oldCurPoint.P.X;
                                                if (Math.Abs(dist) < 1) tempnewp.X = oldCurPoint.P.X + 1 * Math.Sign(dist);
                                                dist = tempnewp.Y - oldCurPoint.P.Y;
                                                if (Math.Abs(dist) < 1) tempnewp.Y = oldCurPoint.P.Y + 1 * Math.Sign(dist);
                                                using (Brush aGradientBrush = new LinearGradientBrush(oldCurPoint.P, tempnewp, Color.FromArgb(10, oldCurPoint.C), Color.FromArgb(10, newc))) {
                                                    using (Pen aGradientPen = new Pen(aGradientBrush)) {
                                                        //var p1 = new PointF(oldCurPoint.P.X / 100, oldCurPoint.P.Y / 100);
                                                        //var p2 = new PointF(curpoint.P.X / 100, curpoint.P.Y / 100);
                                                        //p1 = new PointF(p1.X + curpoint.P.X - p2.X, p1.Y + curpoint.P.Y - p2.Y);
                                                        gfx.DrawLine(aGradientPen, oldCurPoint.P, curpoint.P); //p1, curpoint.P);  
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (x >= 0 && y >= 0 && x < FieldSize.Width && y < FieldSize.Height) {
                                    var newc = Color.FromArgb(Math.Min(255, (count+1)*(count+1)), //50 + Math.Min(205, (count + 1) * 3),
                                                              (c.R * count + curpoint.C.R) / (count + 1),
                                                              (c.G * count + curpoint.C.G) / (count + 1),
                                                              (c.B * count + curpoint.C.B) / (count + 1));
                                    if (!newc.ARGBEquals(c)) {
                                        _bmp.SetPixel(x, y, newc);
                                        _mixCounters[x, y].Color = newc;
                                    }
                                }
                            }
                            else {
                                if (DrawLines) {
                                    using (var gfx = Graphics.FromImage(_bmp)) {
                                        if (oldCurPoint != null && !oldCurPoint.P.EqualTo(curpoint.P)) { // don't draw a line the first time since there's no previous point to draw a line from.
                                            var tempnewp = curpoint.P;
                                            float angle;
                                            PointF center;
                                            float radius;
                                            if (PtSelection == PointSelection.PickTwo && PickTwoMovement == TwoPointMovement.Along3PointArc) {
                                                try {
                                                    using (var path = GetArcPath(oldCurPoint.P, target.tp1.P, target.tp2.P, TravelDist)) {
                                                        if (!path.PathPoints[path.PathPoints.Length - 1].EqualTo(curpoint.P)) throw new Exception("Insane!");
                                                        using (Brush aGradientBrush = new LinearGradientBrush(oldCurPoint.P, tempnewp, oldCurPoint.C, curpoint.C)) {
                                                            using (Pen aGradientPen = new Pen(aGradientBrush)) {
                                                                gfx.DrawPath(aGradientPen, path);
                                                            }
                                                        }
                                                    }
                                                }
                                                catch {
                                                    // do nothing.
                                                    int z = 0;
                                                }
                                            }
                                            else {
                                                var dist = tempnewp.X - oldCurPoint.P.X;
                                                if (Math.Abs(dist) < 1) tempnewp.X = oldCurPoint.P.X + 1 * Math.Sign(dist);
                                                dist = tempnewp.Y - oldCurPoint.P.Y;
                                                if (Math.Abs(dist) < 1) tempnewp.Y = oldCurPoint.P.Y + 1 * Math.Sign(dist);
                                                if (CircularPath) {
                                                    //center = new PointF(FieldSize.Width / 2, FieldSize.Height / 2);
                                                    //gfx.DrawArc(Pens.Blue, center.X - 300, center.Y - 300, 600, 600, 45, 90);
                                                    center = new PointF(oldCurPoint.P.X + (target.tp1.P.X - oldCurPoint.P.X) / 2, oldCurPoint.P.Y + (target.tp1.P.Y - oldCurPoint.P.Y) / 2);
                                                    radius = center.DistanceTo(oldCurPoint.P);
                                                    angle = AngleBetweenTwoPoints(center, new PointF(center.X + radius, center.Y), oldCurPoint.P);
                                                    using (Brush aGradientBrush = new LinearGradientBrush(oldCurPoint.P, tempnewp, oldCurPoint.C, curpoint.C)) {
                                                        using (Pen aGradientPen = new Pen(aGradientBrush)) {
                                                            gfx.DrawArc(aGradientPen, new RectangleF(center.X - radius, center.Y - radius, radius * 2, radius * 2), angle, 360 * TravelDist);
                                                        }
                                                    }
                                                    //int xx = (int)Math.Round(oldCurPoint.P.X);
                                                    //int yy = (int)Math.Round(oldCurPoint.P.Y);
                                                    //gfx.FillEllipse(Brushes.Red, new Rectangle(xx - 4, yy - 4, 8, 8));
                                                    //xx = (int)Math.Round(curpoint.P.X);
                                                    //yy = (int)Math.Round(curpoint.P.Y);
                                                    //gfx.FillEllipse(Brushes.Green, new Rectangle(xx - 4, yy - 4, 8, 8));
                                                    //var tempnewp2 = curpoint.P;
                                                    //if (curpoint.P.RGBEquals(target.tp1.P)) {
                                                    //    int sign = target.tp1.P.X > oldCurPoint.P.X || Math.Abs(target.tp1.P.X - oldCurPoint.P.X) < 0.01 ? -1 : 1;
                                                    //    tempnewp2 = Math.Abs(target.tp1.P.X - oldCurPoint.P.X) < 0.01 ? new PointF(center.X - radius, center.Y) : new PointF(center.X, center.Y + radius * sign);
                                                    //}
                                                    //using (var path = GetArcPath(oldCurPoint.P, tempnewp2, target.tp1.P, TravelDist)) {
                                                    //    if (!path.PathPoints[path.PathPoints.Length - 1].RGBEquals(curpoint.P) || !path.PathPoints[0].RGBEquals(oldCurPoint.P)) throw new Exception("Insane!");
                                                    //    using (Brush aGradientBrush = new LinearGradientBrush(oldCurPoint.P, tempnewp, oldCurPoint.C, curpoint.C)) {
                                                    //        using (Pen aGradientPen = new Pen(aGradientBrush)) {
                                                    //            gfx.DrawPath(aGradientPen, path);
                                                    //        }
                                                    //    }
                                                    //}
                                                    if (HilitePoints) {
                                                        foreach (var p in _points) {
                                                            var c = Color.FromArgb(128, p.C.R, p.C.G, p.C.B);
                                                            gfx.DrawEllipse(new Pen(c, 1), p.P.X - 9, p.P.Y - 9, 18, 18);
                                                        }
                                                    }
                                                }
                                                else {
                                                    using (Brush aGradientBrush = new LinearGradientBrush(oldCurPoint.P, tempnewp, oldCurPoint.C, curpoint.C)) {
                                                        using (Pen aGradientPen = new Pen(aGradientBrush)) {
                                                            gfx.DrawLine(aGradientPen, oldCurPoint.P, curpoint.P);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (x >= 0 && y >= 0 && x < FieldSize.Width && y < FieldSize.Height) {
                                    _bmp.SetPixel(x, y, curpoint.C);
                                    _mixCounters[x, y].Color = curpoint.C;
                                    _mixCounters[x, y].Count++;
                                }
                                //_bmp.Save("test.png");
                            }
                            ++Iterations;
                            if (MaxIterations != 0 && Iterations == MaxIterations) Running = false;
                        }
                    }
                    Thread.Sleep(0);
                }
            }
            finally {
                Stopped?.Invoke(this, EventArgs.Empty);
            }
        }
        public JSONObject ToJSON() {
            var parray = new JSONArray();
            lock (_points) {
                foreach (var p in _points) {
                    parray.put(p.toJSON());
                }
            }
            return new JSONObject().put("Points", parray);
        }
        public void Zoom(RectangleF rect) {
            lock (_points) {
                var dx = _fieldRect.X - rect.X;
                var dy = _fieldRect.Y - rect.Y;
                foreach (var p in _points) p.P = new PointF(p.P.X + dx, p.P.Y + dy);
                float ratio = Math.Max(rect.Width / _fieldRect.Width, rect.Height / _fieldRect.Height);
                if (Math.Abs(ratio - 1) > 0.01) {
                    foreach (var p in _points) p.P = new PointF(p.P.X / ratio, p.P.Y / ratio);
                    //_fieldRect = new RectangleF(_fieldRect.Location, new SizeF(_fieldRect.Width / ratio, _fieldRect.Height / ratio));
                }
                CalcMinMaxDist();
            }
            Clear();
        }
        protected override void OnMouseDown(MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                _mouseDown = true;
                _dragRect = new Rectangle(e.Location, new Size(1, 1));
                _dragCorner = new Point(1, 1);
            }
            else {
                var mp = new PointF(e.X * (float)FieldSize.Width / Width, e.Y * (float)FieldSize.Height / Height);
                PointInfo pt;
                lock (_points) pt = _points.FirstOrDefault(p => p.P.DistanceTo(mp) < 10);
                SelectedPoint = pt;
                menuPointColor.Visible = menuPointLocation.Visible = menuDeletePoint.Visible = menuSeparator1.Visible = pt != null;
                menuMakePointsSameColor.Visible = menuSeparator2.Visible = Count > 0;
                if (pt != null) {
                    using (var gfx = Graphics.FromImage(menuPointColor.Image)) {
                        gfx.FillRectangle(new SolidBrush(pt.C), 0, 0, menuPointColor.Image.Width, menuPointColor.Image.Height);
                    }
                    menuPointLocation.Text = "(" + pt.P.X + ", " + pt.P.Y + ")";
                }
            }
            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if (_pointMenuOpen) return;
            if (_mouseDown) {
                Cursor = Cursors.SizeAll;
                if (_dragCorner.X > 0 && e.X < _dragRect.X || _dragCorner.X < 0 && e.X >= _dragRect.Right) _dragCorner = new Point(_dragCorner.X * -1, _dragCorner.Y);
                if (_dragCorner.Y > 0 && e.Y < _dragRect.Y || _dragCorner.Y < 0 && e.Y >= _dragRect.Bottom) _dragCorner = new Point(_dragCorner.X, _dragCorner.Y * -1);
                var newloc = new Point(_dragCorner.X < 0 ? e.X : _dragRect.X, _dragCorner.Y < 0 ? e.Y : _dragRect.Y);
                var newsize = new Size(_dragCorner.X < 0 ? _dragRect.Right - e.X : e.X - _dragRect.X, _dragCorner.Y < 0 ? _dragRect.Bottom - e.Y : e.Y - _dragRect.Y);
                _dragRect = new Rectangle(newloc, newsize);
                Invalidate();
            }
            else {
                var mp = new PointF(e.X * (float)FieldSize.Width / Width, e.Y * (float)FieldSize.Height / Height);
                lock (_points) Cursor = _points.Any(p => p.P.DistanceTo(mp) < 10) ? Cursors.Cross : Cursors.Arrow;
            }
        }
        protected override void OnMouseUp(MouseEventArgs e) {
            if (_mouseDown) {
                var ratios = new SizeF((float)FieldSize.Width / Width, (float)FieldSize.Height / Height);
                Zoom(new RectangleF(_dragRect.X * ratios.Width, _dragRect.Y * ratios.Height, _dragRect.Width * ratios.Width, _dragRect.Height * ratios.Height));
                _mouseDown = false;
                _dragRect = Rectangle.Empty;
                Cursor = Cursors.Arrow;
                Invalidate();
            }
            base.OnMouseUp(e);
        }
        protected override void OnPaint(PaintEventArgs e) {
            if (_bmp == null) {
                base.OnPaint(e);
                return;
            }
            lock (_bmp) {
                using (var bmp = new Bitmap(_bgImage)) {
                    using (var gfx = Graphics.FromImage(bmp)) {
                        gfx.DrawImage(_bmp, new Rectangle(0, 0, FieldSize.Width, FieldSize.Height), new Rectangle(0, 0, FieldSize.Width, FieldSize.Height), GraphicsUnit.Pixel);
                    }
                    e.Graphics.DrawImage(bmp, new Rectangle(0, 0, Width, Height), new Rectangle(0, 0, FieldSize.Width, FieldSize.Height), GraphicsUnit.Pixel);
                }
            }
            if (HilitePoints) {
                e.Graphics.DrawImage(_hilites, new Rectangle(0, 0, Width, Height), new Rectangle(0, 0, _hilites.Width, _hilites.Height), GraphicsUnit.Pixel);
            }
            if (_dragRect != Rectangle.Empty) {
                e.Graphics.DrawRectangle(Pens.Black, _dragRect.X, _dragRect.Y, _dragRect.Width, _dragRect.Height);
                var pen = new Pen(Color.White, 1) { DashPattern = new float[] { 4, 4 } };
                e.Graphics.DrawRectangle(pen, _dragRect.X, _dragRect.Y, _dragRect.Width, _dragRect.Height);
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e) {
            if (_bmp == null) base.OnPaintBackground(e);
            else if (_paintBackground) e.Graphics.DrawImage(_bgImage, new Rectangle(0, 0, Width, Height), new Rectangle(0, 0, FieldSize.Width, FieldSize.Height), GraphicsUnit.Pixel);
            _paintBackground = false;
        }
        private void CalcMinMaxDist() {
            _maxPointDist = 0;
            _minPointDist = float.MaxValue;
            foreach (var p1 in _points) {
                foreach (var p2 in _points) {
                    if (p2 == p1) continue;
                    var d = p2.P.DistanceTo(p1.P);
                    if (d < _minPointDist) _minPointDist = d;
                    if (d > _maxPointDist) _maxPointDist = d;
                }
            }
        }
        private PointF FindArcTerminus(PointF a, PointF b, PointF c, float sweepRatio) {
            using (var path = GetArcPath(a, b, c, sweepRatio)) {
                return path.PathPoints[path.PathPoints.Length - 1];
            }
        }
        private void GenerateHilitesImage() {
            lock (_bmp) _hilites = new Bitmap(_bmp.Width, _bmp.Height);
            using (var gfx = Graphics.FromImage(_hilites)) {
                gfx.FillRectangle(Brushes.Transparent, 0, 0, _hilites.Width, _hilites.Height);
                lock (_points) {
                    foreach (var p in _points) {
                        var c = Color.FromArgb(128, p.C.R, p.C.G, p.C.B);
                        gfx.FillEllipse(new SolidBrush(c), p.P.X - 10, p.P.Y - 10, 20, 20);
                    }
                }
            }
        }
        //private PointInfo CalculateSpidermanPoint(PointF curpt, PointInfo center, PointInfo target) { //, bool clockwise) {
        //    var normalctr = new PointF(0, 0);
        //    var normalcurpt = new PointF(curpt.X - center.P.X, curpt.Y - center.P.Y);
        //    var normaltarget = new PointF(target.P.X - center.P.X, target.P.Y - center.P.Y);
        //    var distToTarget = normalctr.DistanceTo(normaltarget);
        //    var distToCurpt = normalctr.DistanceTo(normalcurpt);
        //    var unitvect = new PointF(normaltarget.X / distToTarget, normaltarget.Y / distToTarget);
        //    var pt1 = new PointF(unitvect.X * distToCurpt + center.P.X, unitvect.Y * distToCurpt + center.P.Y);
        //    var pt2 = new PointF(unitvect.X * -1 * distToCurpt + center.P.X, unitvect.Y * -1 * distToCurpt + center.P.Y);
        //    //var cw = RotatePoint(curpt, center.P, 0.01F);
        //    //var ccw = RotatePoint(curpt, center.P, -0.01F);
        //    //var cwdist = pt1.DistanceTo(cw);
        //    //var ccwdist = pt1.DistanceTo(ccw);
        //    //var pt = clockwise ? (cwdist < ccwdist ? pt1 : pt2) : (cwdist < ccwdist ? pt2 : pt1);
        //    var pt = pt1.DistanceTo(target.P) < pt2.DistanceTo(target.P) ? pt1 : pt2;
        //    var distToNewPt = center.P.DistanceTo(pt);
        //    if (Math.Abs(distToNewPt - distToCurpt) > 0.001) throw new Exception("Something's wrong with my logic.");
        //    var centerratio = distToNewPt / distToTarget;
        //    var targetratio = 1 - centerratio;
        //    int r = Math.Min(255, Math.Max(0, (int)Math.Round(center.C.R * centerratio + target.C.R * targetratio)));
        //    int g = Math.Min(255, Math.Max(0, (int)Math.Round(center.C.G * centerratio + target.C.G * targetratio)));
        //    int b = Math.Min(255, Math.Max(0, (int)Math.Round(center.C.B * centerratio + target.C.B * targetratio)));
        //    var newcolor = Color.FromArgb(r, g, b);
        //    return new PointInfo(pt, newcolor);
        //}
        //private bool Colinear(PointF p1, PointF p2, PointF p3) {
        //    throw new NotImplementedException();
        //}
        private GraphicsPath GetArcPath(PointF a, PointF b, PointF c, float sweepRatio) {
            var a1 = a;
            var b1 = b;
            var c1 = c;
            while (Math.Abs(a1.X - b1.X) < 0.1) b1 = new PointF(b1.X + 0.01F, b1.Y);
            while (Math.Abs(a1.Y - b1.Y) < 0.1) b1 = new PointF(b1.X, b1.Y + 0.01F);
            while (Math.Abs(b1.X - c1.X) < 0.1) b1 = new PointF(b1.X + 0.01F, b1.Y);
            while (Math.Abs(b1.Y - c1.Y) < 0.1) b1 = new PointF(b1.X, b1.Y + 0.01F);
            double d;
            do {
                d = 2 * (a1.X - c1.X) * (c1.Y - b1.Y) + 2 * (b1.X - c1.X) * (a1.Y - c1.Y);
                if (d == 0) {
                    b1 = new PointF(b1.X + 0.01F, b1.Y + 0.01F);
                    a1 = new PointF(a1.X - 0.01F, a1.Y + 0.01F);
                    c1 = new PointF(c1.X + 0.02F, c1.Y - 0.01F);
                }
            } while (d == 0);
            double m1 = (Math.Pow(a1.X, 2) - Math.Pow(c1.X, 2) + Math.Pow(a1.Y, 2) - Math.Pow(c1.Y, 2));
            double m2 = (Math.Pow(c1.X, 2) - Math.Pow(b1.X, 2) + Math.Pow(c1.Y, 2) - Math.Pow(b1.Y, 2));
            double nx = m1 * (c1.Y - b1.Y) + m2 * (c1.Y - a1.Y);
            double ny = m1 * (b1.X - c1.X) + m2 * (a1.X - c1.X);
            double cx = nx / d;
            double cy = ny / d;
            double dx = cx - a1.X;
            double dy = cy - a1.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            var va = new Vector(a1.X - cx, a1.Y - cy);
            var vb = new Vector(b1.X - cx, b1.Y - cy);
            var vc = new Vector(c1.X - cx, c1.Y - cy);
            var xaxis = new Vector(1, 0);
            float startAngle = (float)Vector.AngleBetween(xaxis, va);
            float sweepAngle = (float)(Vector.AngleBetween(va, vb) + Vector.AngleBetween(vb, vc)) * sweepRatio;
            var path = new GraphicsPath();
            path.AddArc((float)(cx - distance),
                        (float)(cy - distance),
                        (float)(distance * 2),
                        (float)(distance * 2),
                        startAngle,
                        sweepAngle);
            if (float.IsNaN(path.PathPoints[0].X)) {
                int x = 0;
            }
            return path;
        }
        private PointInfo GetNewCurpoint(PointInfo curpoint, NextTargetInfo target) {
            int r, g, b;
            switch (PtSelection) {
                case PointSelection.PickOne:
                    PointF vector;
                    float fraction;
                    float Fraction(float distToTarget) {
                        int effectiveMix = ComboMix < 0 ? 100 : ComboMix;
                        switch (DistMeaning) {
                            default:
                            case DistanceMeaning.FractionDistToTarget:
                                return TravelDist;
                            case DistanceMeaning.FractionMaxDist:
                                float fractionalMax = _maxPointDist * TravelDist;
                                float fmaxval = fractionalMax / distToTarget;
                                return (TravelDist * (100 - effectiveMix) + fmaxval * effectiveMix) / 100;
                            case DistanceMeaning.FractionMinDist:
                                var fractionalMin = _minPointDist * TravelDist;
                                float fminval = fractionalMin / distToTarget;
                                return (TravelDist * (100 - effectiveMix) + fminval * effectiveMix) / 100;
                            case DistanceMeaning.ConstantDist:
                                return (TravelDist * (100 - effectiveMix) + (ConstantDist / distToTarget) * effectiveMix) / 100;
                            //case DistanceMeaning.ComboDist:
                            //    if (ComboMix == 0) return TravelDist;
                            //    fractionalMax = _maxPointDist * TravelDist;
                            //    float fmaxval = fractionalMax / distToTarget;
                            //    if (ComboMix == 100) return fmaxval;
                            //    return (TravelDist * (100 - ComboMix) + fmaxval * ComboMix) / 100;
                        }
                    }
                    void SanityCheck(PointF oldCurPoint, PointF newCurPoint) {
                        float d = oldCurPoint.DistanceTo(newCurPoint);
                        float checkd;
                        float d1 = oldCurPoint.DistanceTo(target.tp1.P) * TravelDist, d2;
                        int effectiveMix = ComboMix < 0 ? 100 : ComboMix;
                        switch (DistMeaning) {
                            default:
                            case DistanceMeaning.FractionDistToTarget:
                                checkd = oldCurPoint.DistanceTo(target.tp1.P) * TravelDist;
                                break;
                            case DistanceMeaning.FractionMaxDist:
                                d2 = _maxPointDist * TravelDist;
                                checkd = (d1 * (100 - effectiveMix) + d2 * effectiveMix) / 100;
                                break;
                            case DistanceMeaning.FractionMinDist:
                                d2 = _minPointDist * TravelDist;
                                checkd = (d1 * (100 - effectiveMix) + d2 * effectiveMix) / 100;
                                break;
                            case DistanceMeaning.ConstantDist:
                                checkd = (d1 * (100 - effectiveMix) + ConstantDist * effectiveMix) / 100;
                                break;
                        }
                        //if (Math.Abs(d - checkd) > 0.01) throw new Exception("Insane");
                    }
                    switch (PickOneMovement) {
                        case OnePointMovement.DirectToPoint:
                            if (curpoint != null) {
                                vector = new PointF(target.tp1.P.X - curpoint.P.X, target.tp1.P.Y - curpoint.P.Y);
                                if (CircularPath) {
                                    PointF center = new PointF(curpoint.P.X + vector.X / 2, curpoint.P.Y + vector.Y / 2);
                                    fraction = TravelDist - (float)Math.Floor(TravelDist); // force it to be < 1.
                                    r = Math.Min(target.tp1.C.R, Math.Max(0, (int)Math.Round(curpoint.C.R + (target.tp1.C.R - curpoint.C.R) * fraction)));
                                    g = Math.Min(target.tp1.C.G, Math.Max(0, (int)Math.Round(curpoint.C.G + (target.tp1.C.G - curpoint.C.G) * fraction)));
                                    b = Math.Min(target.tp1.C.B, Math.Max(0, (int)Math.Round(curpoint.C.B + (target.tp1.C.B - curpoint.C.B) * fraction)));
                                    curpoint = new PointInfo(RotatePoint(curpoint.P, center, 360F * TravelDist), Color.FromArgb(r, g, b));
                                }
                                else {
                                    fraction = Fraction(curpoint.P.DistanceTo(target.tp1.P));
                                    r = Math.Min(target.tp1.C.R, Math.Max(0, (int)Math.Round(curpoint.C.R + (target.tp1.C.R - curpoint.C.R) * fraction)));
                                    g = Math.Min(target.tp1.C.G, Math.Max(0, (int)Math.Round(curpoint.C.G + (target.tp1.C.G - curpoint.C.G) * fraction)));
                                    b = Math.Min(target.tp1.C.B, Math.Max(0, (int)Math.Round(curpoint.C.B + (target.tp1.C.B - curpoint.C.B) * fraction)));
                                    var cp = curpoint;
                                    curpoint = new PointInfo(new PointF(curpoint.P.X + vector.X * fraction, curpoint.P.Y + vector.Y * fraction), Color.FromArgb(r, g, b));
                                    SanityCheck(cp.P, curpoint.P);
                                }
                            }
                            else curpoint = target.tp1;
                            break;
                        case OnePointMovement.ToAvgLastN:
                            _prevPoints.Add(target.tp1);
                            if (_prevPoints.Count > MoveN) _prevPoints.RemoveAt(0);
                            bool removelast = false;
                            if (curpoint != null) {
                                if (IncludeCurPutInMoveCalc) {
                                    _prevPoints.Add(curpoint);
                                    removelast = true;
                                }
                                var targetp = new PointF(_prevPoints.Average(p => p.P.X), _prevPoints.Average(p => p.P.Y));
                                vector = new PointF(targetp.X - curpoint.P.X, targetp.Y - curpoint.P.Y);
                                fraction = Fraction(curpoint.P.DistanceTo(targetp));
                                var cp = curpoint;
                                curpoint = new PointInfo(new PointF(curpoint.P.X + vector.X * fraction, curpoint.P.Y + vector.Y * fraction),
                                                         Color.FromArgb(Math.Min(target.tp1.C.R, Math.Max(0, (int)Math.Round(curpoint.C.R + (_prevPoints.Average(p => p.C.R) - curpoint.C.R) * fraction))),
                                                                        Math.Min(target.tp1.C.G, Math.Max(0, (int)Math.Round(curpoint.C.G + (_prevPoints.Average(p => p.C.G) - curpoint.C.G) * fraction))),
                                                                        Math.Min(target.tp1.C.B, Math.Max(0, (int)Math.Round(curpoint.C.B + (_prevPoints.Average(p => p.C.B) - curpoint.C.B) * fraction)))));
                                SanityCheck(cp.P, curpoint.P);
                            }
                            else curpoint = target.tp1;
                            if (removelast) _prevPoints.RemoveAt(_prevPoints.Count - 1);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case PointSelection.PickTwo:
                    Color midclr;
                    switch (PickTwoMovement) {
                        case TwoPointMovement.MoveTowardMidpoint:
                            var midpt = new PointF((target.tp1.P.X + target.tp2.P.X) / 2, (target.tp1.P.Y + target.tp2.P.Y) / 2);
                            midclr = Color.FromArgb((target.tp1.C.R + target.tp2.C.R) / 2, (target.tp1.C.G + target.tp2.C.G) / 2, (target.tp1.C.B + target.tp2.C.B) / 2);
                            if (curpoint != null) {
                                r = Math.Min(255, Math.Max(0, (int)Math.Round(curpoint.C.R + (midclr.R - curpoint.C.R) * TravelDist)));
                                g = Math.Min(255, Math.Max(0, (int)Math.Round(curpoint.C.G + (midclr.G - curpoint.C.G) * TravelDist)));
                                b = Math.Min(255, Math.Max(0, (int)Math.Round(curpoint.C.B + (midclr.B - curpoint.C.B) * TravelDist)));
                                curpoint = new PointInfo(new PointF(curpoint.P.X + (midpt.X - curpoint.P.X) * TravelDist, curpoint.P.Y + (midpt.Y - curpoint.P.Y) * TravelDist),
                                                         Color.FromArgb(r, g, b));
                            }
                            else {
                                curpoint = new PointInfo(midpt, midclr);
                            }
                            break;
                        case TwoPointMovement.Along3PointArc:
                            // The first time, just pick one of the two points to be the current point.
                            if (curpoint == null) {
                                curpoint = _rand.Next(2) == 0 ? target.tp1 : target.tp2;
                            }
                            else {
                                midclr = Color.FromArgb((target.tp1.C.R + target.tp2.C.R) / 2, (target.tp1.C.G + target.tp2.C.G) / 2, (target.tp1.C.B + target.tp2.C.B) / 2);
                                r = Math.Min(255, Math.Max(0, (int)Math.Round(curpoint.C.R + (midclr.R - curpoint.C.R) * TravelDist)));
                                g = Math.Min(255, Math.Max(0, (int)Math.Round(curpoint.C.G + (midclr.G - curpoint.C.G) * TravelDist)));
                                b = Math.Min(255, Math.Max(0, (int)Math.Round(curpoint.C.B + (midclr.B - curpoint.C.B) * TravelDist)));
                                //var oldcp = curpoint;
                                curpoint = new PointInfo(FindArcTerminus(curpoint.P, target.tp1.P, target.tp2.P, TravelDist), Color.FromArgb(r, g, b));
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return curpoint;
        }
        private NextTargetInfo GetNextTarget(PointInfo curpoint) {
            PointInfo PickPointAtRandom(out int index, PointInfo except = null) {
                PointInfo pi;
                // Pick a point at random.  Make sure it's not the same point as the current point.
                // Also, if except was passed, make sure it's not the same point.
                do {
                    pi = _points[index = _rand.Next(_points.Count)];
                } while (curpoint != null && curpoint.P.EqualTo(pi.P) || except != null && pi.P.EqualTo(except.P));
                return pi;
            }
            lock (_points) {
                // Pick point(s)
                PointInfo targetp;
                int targetpIndex;
                switch (PtSelection) {
                    case PointSelection.PickOne:
                        switch (PickOneSelection) {
                            case OnePointSelection.Random:
                                targetp = PickPointAtRandom(out targetpIndex);
                                return new NextTargetInfo(targetp); //, targetpIndex);
                            case OnePointSelection.Nearest:
                            case OnePointSelection.Farthest:
                                // The first time, pick a new point at random,
                                // after that, pick a new point that either closest to or farthest from the current point.
                                if (curpoint == null) targetp = PickPointAtRandom(out targetpIndex);
                                else {
                                    // sort points according to how close they are to the current point.
                                    _points.Sort((p1, p2) => {
                                                     var d1 = p1.P.DistanceTo(curpoint.P);
                                                     var d2 = p2.P.DistanceTo(curpoint.P);
                                                     return d1 < d2 ? -1 : d1 > d2 ? 1 : 0;
                                                 });
                                    // Pick a new point at random among nearest/farthest points.
                                    targetpIndex = PickOneSelection == OnePointSelection.Nearest ? _rand.Next(PickN) : _rand.Next(_points.Count - PickN, _points.Count);
                                    targetp = _points[targetpIndex];
                                    //// if movement type is AlongLast3Arc, we cannot let any of the last 3 points be the same point.
                                    //if (PickOneMovement == OnePointMovement.AlongLast3Arc) {
                                    //    var prevpoints = _prevPoints;
                                    //    if (prevpoints.Count == 3) prevpoints = _prevPoints.GetRange(1, 2);
                                    //    if (prevpoints.Any(p => p.P.RGBEquals(targetp.P))) return GetNextTarget(curpoint);
                                    //}
                                    return new NextTargetInfo(targetp); //, targetpIndex);
                                }
                                return new NextTargetInfo(targetp); //, targetpIndex);
                            case OnePointSelection.WithinRadius:
                                // The first time, pick a point at random,
                                // after that, pick among points within the PickRadius.
                                // If there are no points within the PickRadius, pick at random again.
                                if (curpoint == null) targetp = PickPointAtRandom(out targetpIndex);
                                else {
                                    var radpoints = _points.Where(p => !p.P.EqualTo(curpoint.P) && p.P.DistanceTo(curpoint.P) <= PickRadius).ToArray();
                                    if (radpoints.Length == 0) {
                                        targetp = _points[_rand.Next(_points.Count)]; //targetpIndex = _rand.Next(_points.Count)];
                                    }
                                    else {
                                        targetp = radpoints[_rand.Next(radpoints.Length)];
                                        //targetpIndex = _points.FindIndex(p => p == targetp);
                                    }
                                }
                                return new NextTargetInfo(targetp); //, targetpIndex);
                            default:
                                throw new Exception("Invalid PickOneSelection");
                        }
                    case PointSelection.PickTwo:
                        PointInfo targetp2;
                        int targetp2Index;
                        switch (PickTwoSelection) {
                            case TwoPointSelection.Random:
                                // pick two points at random, making sure the two points are different.
                                targetp = PickPointAtRandom(out targetpIndex);
                                targetp2 = PickPointAtRandom(out targetp2Index, targetp);
                                return new NextTargetInfo(targetp, targetp2); //, targetpIndex, targetp2Index);
                            case TwoPointSelection.Nearest:
                            case TwoPointSelection.Farthest:
                                // The first time, pick two new points at random.
                                // after that, pick points that either closest to or farthest from the current point.
                                if (curpoint == null) {
                                    targetp = PickPointAtRandom(out targetpIndex);
                                    targetp2 = PickPointAtRandom(out targetp2Index, targetp);
                                }
                                else {
                                    // sort points according to how close they are to the current point.
                                    _points.Sort((p1, p2) => {
                                                     var d1 = p1.P.DistanceTo(curpoint.P);
                                                     var d2 = p2.P.DistanceTo(curpoint.P);
                                                     return d1 < d2 ? -1 : d1 > d2 ? 1 : 0;
                                                 });
                                    // Pick new points at random among nearest/farthest points.
                                    targetpIndex = PickTwoSelection == TwoPointSelection.Nearest ? _rand.Next(PickN) : _rand.Next(_points.Count - PickN, _points.Count);
                                    targetp = _points[targetpIndex];
                                    do {
                                        targetp2Index = PickTwoSelection == TwoPointSelection.Nearest ? _rand.Next(PickN) : _rand.Next(_points.Count - PickN, _points.Count);
                                        targetp2 = _points[targetp2Index];
                                    } while (targetp2Index == targetpIndex);
                                }
                                return new NextTargetInfo(targetp, targetp2); //, targetpIndex, targetp2Index);
                            case TwoPointSelection.WithinRadius:
                                // The first time, pick a point at random,
                                // after that, pick among points within the PickRadius.
                                // If there are not two points within the PickRadius, pick at random again as needed.
                                if (curpoint == null) {
                                    targetp = PickPointAtRandom(out targetpIndex);
                                    targetp2 = PickPointAtRandom(out targetp2Index, targetp);
                                }
                                else {
                                    var radpoints = _points.Where(p => p.P.DistanceTo(curpoint.P) <= PickRadius).ToArray();
                                    targetp = radpoints.Length == 0 ? _points[_rand.Next(_points.Count)] : radpoints[_rand.Next(radpoints.Length)];
                                    do {
                                        targetp2 = radpoints.Length < 2 ? _points[_rand.Next(_points.Count)] : radpoints[_rand.Next(radpoints.Length)];
                                    } while (targetp2.P.EqualTo(targetp.P));
                                    //targetpIndex = _points.FindIndex(p => p == targetp);
                                    //targetp2Index = _points.FindIndex(p => p == targetp2);
                                }
                                return new NextTargetInfo(targetp, targetp2); //, targetpIndex, targetp2Index);
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    default:
                        throw new Exception("Invalid PtSelection value");
                }
            }
        }
        private void InitializeComponent() {
            this.components = new Container();
            this.cmenuPoint = new ContextMenuStrip(this.components);
            this.menuSave = new ToolStripMenuItem();
            this.menuSeparator2 = new ToolStripSeparator();
            this.menuMakePointsSameColor = new ToolStripMenuItem();
            this.menuSeparator1 = new ToolStripSeparator();
            this.menuPointLocation = new ToolStripTextBox();
            this.menuPointColor = new ToolStripMenuItem();
            this.saveImageDialog = new SaveFileDialog();
            this.colorDialog = new ColorDialog();
            this.menuDeletePoint = new ToolStripMenuItem();
            this.cmenuPoint.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmenuPoint
            // 
            this.cmenuPoint.Items.AddRange(new ToolStripItem[] {
                                                                   this.menuSave,
                                                                   this.menuSeparator2,
                                                                   this.menuMakePointsSameColor,
                                                                   this.menuSeparator1,
                                                                   this.menuPointLocation,
                                                                   this.menuPointColor,
                                                                   this.menuDeletePoint
                                                               });
            this.cmenuPoint.Name = "cmenuPoint";
            this.cmenuPoint.Size = new Size(236, 151);
            this.cmenuPoint.Closed += new ToolStripDropDownClosedEventHandler(this.cmenuPoint_Closed);
            this.cmenuPoint.Opened += new EventHandler(this.cmenuPoint_Opened);
            // 
            // menuSave
            // 
            this.menuSave.Name = "menuSave";
            this.menuSave.Size = new Size(235, 22);
            this.menuSave.Text = "Save image";
            this.menuSave.Click += new EventHandler(this.menuSave_Click);
            // 
            // menuSeparator2
            // 
            this.menuSeparator2.Name = "menuSeparator2";
            this.menuSeparator2.Size = new Size(232, 6);
            // 
            // menuMakePointsSameColor
            // 
            this.menuMakePointsSameColor.Image = Resources.dummy_color;
            this.menuMakePointsSameColor.Name = "menuMakePointsSameColor";
            this.menuMakePointsSameColor.Size = new Size(235, 22);
            this.menuMakePointsSameColor.Text = "Make all points the same color";
            this.menuMakePointsSameColor.Click += new EventHandler(this.menuMakePointsSameColor_Click);
            // 
            // menuSeparator1
            // 
            this.menuSeparator1.Name = "menuSeparator1";
            this.menuSeparator1.Size = new Size(232, 6);
            // 
            // menuPointLocation
            // 
            this.menuPointLocation.Font = new Font("Segoe UI", 9F);
            this.menuPointLocation.Name = "menuPointLocation";
            this.menuPointLocation.Size = new Size(100, 23);
            this.menuPointLocation.Text = "(100, 100)";
            this.menuPointLocation.KeyPress += new KeyPressEventHandler(this.menuPointLocation_KeyPress);
            this.menuPointLocation.KeyUp += new KeyEventHandler(this.menuPointLocation_KeyUp);
            // 
            // menuPointColor
            // 
            this.menuPointColor.BackColor = SystemColors.Control;
            this.menuPointColor.Image = Resources.dummy_color;
            this.menuPointColor.Name = "menuPointColor";
            this.menuPointColor.Size = new Size(235, 22);
            this.menuPointColor.Text = "Color";
            this.menuPointColor.Click += new EventHandler(this.menuPointColor_Click);
            // 
            // saveImageDialog
            // 
            this.saveImageDialog.DefaultExt = "png";
            this.saveImageDialog.Filter = "PNG files|*.png";
            // 
            // menuDeletePoint
            // 
            this.menuDeletePoint.Name = "menuDeletePoint";
            this.menuDeletePoint.Size = new Size(235, 22);
            this.menuDeletePoint.Text = "Delete point";
            this.menuDeletePoint.Click += new EventHandler(this.menuDeletePoint_Click);
            // 
            // PointGamePanel
            // 
            this.ContextMenuStrip = this.cmenuPoint;
            this.Name = "PointGamePanel";
            this.cmenuPoint.ResumeLayout(false);
            this.cmenuPoint.PerformLayout();
            this.ResumeLayout(false);
        }
        //private PointF[] GetSpidermanArcPoints(PointF center, PointF start, PointF end, bool clockwise) {
        //    List<PointF> pts = new List<PointF>();
        //    if (start.RGBEquals(end)) return new[] { end };
        //    PointF curp = start;
        //    float rotinterval = clockwise ? -0.01F : 0.01F;
        //    bool gettingCloser = false;
        //    do {
        //        var dist = curp.DistanceTo(end);
        //        var lastcurp = new Point((int)Math.Round(curp.X), (int)Math.Round(curp.Y));
        //        do {
        //            curp = RotatePoint(curp, center, rotinterval);
        //        } while (lastcurp.X == (int)Math.Round(curp.X) && lastcurp.Y == (int)Math.Round(curp.Y));
        //        if (!gettingCloser && curp.DistanceTo(end) < dist) gettingCloser = true;
        //        else if (gettingCloser && curp.DistanceTo(end) > dist) break;
        //        pts.Add(curp);
        //    } while (!curp.RGBEquals(end, 0.5F));
        //    return pts.ToArray();
        //}
        private static float AngleBetweenTwoPoints(PointF center, PointF p1, PointF p2) {
            // Move all points so the center is at (0,0).
            p1 = new PointF(p1.X - center.X, p1.Y - center.Y);
            p2 = new PointF(p2.X - center.X, p2.Y - center.Y);
            center = new PointF(0, 0);
            // Calc the lengths of the two hypotenuses.
            float diam1 = (float)Math.Sqrt(Math.Pow(p1.X, 2) + Math.Pow(p1.Y, 2));
            float diam2 = (float)Math.Sqrt(Math.Pow(p2.X, 2) + Math.Pow(p2.Y, 2));
            // scale the shorter of the two so that they are of the same length.
            if (diam1 < diam2) {
                p1 = new PointF(p1.X * diam2 / diam1, p1.Y * diam2 / diam1);
            }
            else if (diam2 < diam1) {
                p2 = new PointF(p2.X * diam1 / diam2, p2.Y * diam1 / diam2);
            }
            //// Recalc the lengths and make sure they're the same (this code can be removed when this works).
            diam1 = (float)Math.Sqrt(Math.Pow(p1.X, 2) + Math.Pow(p1.Y, 2));
            //diam2 = (float)Math.Sqrt(Math.Pow(p2.X, 2) + Math.Pow(p2.Y, 2));
            //if (Math.Abs(diam2 - diam1) > EPSILON) throw new Exception("scaling of lines didn't work.");
            // calculate the distance between the two points.  This is the length of the side opposite the center.
            float opp = (float)Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
            // the angle is twice the length of the sides of two right triangles formed by bisecting the triangle made by the three points.
            float angle = 2 * ((float)Math.Asin((opp / 2) / diam1) * 180 / (float)Math.PI);
            // Now try rotating p1 through the angle calculated and see if we get p2.  If we don't, it means the angle needs to be reversed.
            if (RotatePoint(p1, center, angle).EqualTo(p2)) return angle;
            return -1 * angle;
        }
        /// <summary>
        ///     Rotates one point arount another one
        /// </summary>
        private static PointF RotatePoint(PointF pointToRotate, PointF centerPoint, float angleInDegrees) {
            float angleInRadians = angleInDegrees * (float)(Math.PI / 180);
            float cosTheta = (float)Math.Cos(angleInRadians);
            float sinTheta = (float)Math.Sin(angleInRadians);
            return new PointF {
                                  X = cosTheta * (pointToRotate.X - centerPoint.X) -
                                      sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X,
                                  Y = sinTheta * (pointToRotate.X - centerPoint.X) +
                                      cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y
                              };
        }
        private Bitmap _bgImage;
        private Bitmap _bmp;
        private Point _dragCorner;
        private Rectangle _dragRect;
        private RectangleF _fieldRect = RectangleF.Empty;
        private Bitmap _hilites;
        private float _maxPointDist;
        private float _minPointDist;
        //private int[,] _mixCounters;
        private MixInfo[,] _mixCounters;
        private bool _mouseDown;
        private bool _paintBackground;
        private bool _pointMenuOpen;
        private readonly List<PointInfo> _points = new List<PointInfo>();
        private readonly List<PointInfo> _prevPoints = new List<PointInfo>();
        private readonly Random _rand = new Random();
        private readonly Color[] _replacementColors = {
                                                          Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Cyan,
                                                          //Color.Purple, Color.Brown, Color.Lime, Color.FromArgb(0, 201, 130), Color.BlueViolet, Color.Coral,
                                                          //Color.CornflowerBlue, Color.DeepPink,
                                                          Color.Black
                                                      };
        private ContextMenuStrip cmenuPoint;
        private ColorDialog colorDialog;
        private IContainer components;
        private ToolStripMenuItem menuDeletePoint;
        private ToolStripMenuItem menuMakePointsSameColor;
        private ToolStripMenuItem menuPointColor;
        private ToolStripTextBox menuPointLocation;
        private ToolStripMenuItem menuSave;
        private ToolStripSeparator menuSeparator1;
        private ToolStripSeparator menuSeparator2;
        private SaveFileDialog saveImageDialog;
        public enum DistanceMeaning {
            FractionDistToTarget,
            FractionMaxDist,
            FractionMinDist,
            ConstantDist
        }
        public enum OnePointMovement {
            // ReSharper disable UnusedMember.Global
            None = -1,
            // ReSharper restore UnusedMember.Global
            DirectToPoint = 0,
            ToAvgLastN = 1,
            //AlongLast3Arc = 2
        }
        public enum OnePointSelection {
            // ReSharper disable UnusedMember.Global
            None = -1,
            // ReSharper restore UnusedMember.Global
            Random = 0,
            Nearest = 1,
            Farthest = 2,
            WithinRadius = 3
        }
        public enum PointSelection {
            // ReSharper disable UnusedMember.Global
            None = 0,
            // ReSharper restore UnusedMember.Global
            PickOne = 1,
            PickTwo = 2
        }
        public enum TwoPointMovement {
            None = -1,
            MoveTowardMidpoint = 0,
            Along3PointArc
        }
        public enum TwoPointSelection {
            // ReSharper disable UnusedMember.Global
            None = -1,
            // ReSharper restore UnusedMember.Global
            Random = 0,
            Nearest = 1,
            Farthest = 2,
            WithinRadius = 3
        }
        private struct MixInfo {
            public int Count;
            public Color Color; // {
            //    get => _color;
            //    set {
            //        if (value.ARGBEquals(_color)) return;
            //        _color = value;
            //        Changed = true;
            //    }
            //}
            //public bool Changed { get; set; }
            //private Color _color;
        }
        private class NextTargetInfo {
            public NextTargetInfo(PointInfo p1) { //, int i1) {
                tp1 = p1;
                tp2 = null;
                //tp1Index = i1;
                //tp2Index = -1;
            }
            public NextTargetInfo(PointInfo p1, PointInfo p2) { //, int i1, int i2) {
                tp1 = p1;
                tp2 = p2;
                //tp1Index = i1;
                //tp2Index = i2;
            }
            public readonly PointInfo tp1;
            //public int tp1Index;
            public readonly PointInfo tp2;
            //public int tp2Index;
        }
    }
}