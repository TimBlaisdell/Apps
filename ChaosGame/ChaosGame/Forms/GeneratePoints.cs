using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using JSON;

namespace ChaosGame {
    public partial class GeneratePoints : Form {
        public GeneratePoints(Size fieldsize, int margin, JSONObject persistValues) {
            InitializeComponent();
            _margin = margin;
            cboxPattern.Items.Clear();
            cboxPattern.Items.AddRange(_patterns.ToArray<object>());
            if (persistValues != null) PersistentValues = persistValues;
            Points = new List<PointInfo>();
            _fieldSize = fieldsize;
            if (persistValues == null) {
                cboxPattern.SelectedIndex = 0;
                txtCenter.Text = txtPatternCenter.Text = (_fieldSize.Width / 2) + ", " + (_fieldSize.Height / 2);
                // ReSharper disable once PossibleLossOfFraction
                numRadius.Value = _fieldSize.Width / 2 - 1;
                txtTopLeft.Text = "0, 0";
                txtRectSize.Text = txtPatternSize.Text = _fieldSize.Width + ", " + _fieldSize.Height;
            }
            else LoadPersistentValues();
        }
        public JSONObject PersistentValues { get; } = new JSONObject();
        public List<PointInfo> Points { get; }
        private void btnCreate_Click(object sender, EventArgs e) {
            switch (tabControl.SelectedTab.Text) {
                case "Circular":
                    CreateCircularPoints();
                    break;
                case "Rectangular":
                    CreateRectPoints();
                    break;
                case "Pattern":
                    CreatePatternPoints();
                    break;
                case "From image":
                    CreateImagePoints();
                    break;
            }
        }
        private void btnLoadImage_Click(object sender, EventArgs e) {
            var curfile = PersistentValues.optString("SelectedImageFile");
            using (var dlg = new OpenFileDialog { Title = "Load pattern image", InitialDirectory = string.IsNullOrEmpty(curfile) ? null : Path.GetDirectoryName(curfile) }) {
                if (dlg.ShowDialog() == DialogResult.OK) {
                    try {
                        var bmp = new Bitmap(dlg.FileName);
                    }
                    catch {
                        MessageBox.Show("Unable to load image from file.", "Error");
                        return;
                    }
                    var ifi = new ImageFileInfo(dlg.FileName);
                    if (!cboxImageFile.Items.Contains(ifi)) {
                        cboxImageFile.Items.Add(ifi);
                        var a = PersistentValues.optJSONArray("RecentImageFiles");
                        if (a == null) PersistentValues.put("RecentImageFiles", a = new JSONArray());
                        a.put(ifi.Path);
                        cboxImageFile.SelectedItem = ifi;
                    }
                    else {
                        cboxImageFile.SelectedItem = ifi;
                    }
                }
            }
        }
        private void btnRemoveImageFromList_Click(object sender, EventArgs e) {
            if (cboxImageFile.Items.Count == 0 || cboxImageFile.SelectedIndex < 0) return;
            var ifi = cboxImageFile.SelectedItem as ImageFileInfo;
            if (ifi == null) return;
            var jarray = PersistentValues.optJSONArray("RecentImageFiles");
            int i = jarray.ToList().IndexOf(ifi.Path);
            if (i >= 0) jarray.RemoveAt(i);
            i = cboxImageFile.SelectedIndex;
            cboxImageFile.Items.RemoveAt(i);
            if (i < cboxImageFile.Items.Count) cboxImageFile.SelectedIndex = i;
            else if (cboxImageFile.Items.Count > 0) cboxImageFile.SelectedIndex = cboxImageFile.Items.Count - 1;
        }
        private void cboxImageFile_SelectedIndexChanged(object sender, EventArgs e) {
            var ifi = cboxImageFile.SelectedItem as ImageFileInfo;
            var path = ifi?.Path ?? "";
            PersistentValues.put("SelectedImageFile", path);
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) {
                pboxImage.BackgroundImage = null;
                MessageBox.Show(string.IsNullOrEmpty(path) ? "No file specified." : "File does not exist.", "Error");
                return;
            }
            try {
                using (var bmp = new Bitmap(path)) {
                    var spacedBmp = new Bitmap(bmp.Width * 10 * 2 - 10, bmp.Height * 10 * 2 - 10);
                    int found = 0;
                    using (var gfx = Graphics.FromImage(spacedBmp)) {
                        for (int y = 0; y < bmp.Height; ++y) {
                            for (int x = 0; x < bmp.Width; ++x) {
                                var c = bmp.GetPixel(x, y);
                                if (c.RGBEquals(Color.White) || c.A < 200) c = Color.White;
                                else ++found;
                                gfx.FillRectangle(new SolidBrush(c), new Rectangle(x * 10 * 2, y * 10 * 2, 10, 10));
                                if (x < bmp.Width - 1) gfx.FillRectangle(Brushes.White, new Rectangle(x * 10 * 2 + 10, y * 10 * 2, 10, 10));
                            }
                            if (y < bmp.Height - 1) {
                                gfx.FillRectangle(Brushes.White, new Rectangle(0, y * 10 * 2 + 10, spacedBmp.Width, 10));
                            }
                        }
                    }
                    pboxImage.BackgroundImage = spacedBmp;
                    lblPointsFound.Text = "Points found: " + found;
                    lblPointsFound.Visible = true;
                }
            }
            catch {
                MessageBox.Show("Unable to load image file.", "Error");
            }
        }
        private void cboxPattern_SelectedIndexChanged(object sender, EventArgs e) {
            PersistentValues.put("Pattern", cboxPattern.SelectedIndex);
        }
        private void chk_CheckedChanged(object sender, EventArgs e) {
            PersistentValues.put("TopCheck", chkTop.Checked);
            PersistentValues.put("BottomCheck", chkBottom.Checked);
            PersistentValues.put("LeftCheck", chkLeft.Checked);
            PersistentValues.put("RightCheck", chkRight.Checked);
        }
        private void chkAddNewPoints_CheckedChanged(object sender, EventArgs e) {
            PersistentValues.put("AddToExisting", chkAddNewPoints.Checked);
        }
        private void numArcSweep_ValueChanged(object sender, EventArgs e) {
            PersistentValues.put("ArcSweep", (int)numArcSweep.Value);
        }
        private void numCircularPoints_ValueChanged(object sender, EventArgs e) {
            PersistentValues.put("CircularPoints", (int)numCircularPoints.Value);
        }
        private void numPatternPoints_ValueChanged(object sender, EventArgs e) {
            PersistentValues.put("PatternPoints", (int)numPatternPoints.Value);
        }
        private void numPointsPerSide_ValueChanged(object sender, EventArgs e) {
            PersistentValues.put("PointsPerSide", (int)numPointsPerSide.Value);
        }
        private void numRadius_ValueChanged(object sender, EventArgs e) {
            PersistentValues.put("Radius", (int)numRadius.Value);
        }
        private void numSpiralStep_ValueChanged(object sender, EventArgs e) {
            PersistentValues.put("SpiralStep", (int)numSpiralStep.Value);
        }
        private void numStartAngle_ValueChanged(object sender, EventArgs e) {
            PersistentValues.put("StartAngle", (int)numStartAngle.Value);
        }
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e) {
            PersistentValues.put("SelectedTab", tabControl.SelectedIndex);
        }
        private void txtCenter_TextChanged(object sender, EventArgs e) {
            PersistentValues.put("CircularCenter", txtCenter.Text);
        }
        private void txtPatternCenter_TextChanged(object sender, EventArgs e) {
            PersistentValues.put("PatternCenter", txtPatternCenter.Text);
        }
        private void txtPatternSize_TextChanged(object sender, EventArgs e) {
            PersistentValues.put("PatternSize", txtPatternSize.Text);
        }
        private void txtRectSize_TextChanged(object sender, EventArgs e) {
            PersistentValues.put("RectSize", txtRectSize.Text);
        }
        private void txtTopLeft_TextChanged(object sender, EventArgs e) {
            PersistentValues.put("TopLeft", txtTopLeft.Text);
        }
        private void CreateCircularPoints() {
            var centerxy = txtCenter.Text.Split(',');
            var center = new PointF(float.Parse(centerxy[0]), float.Parse(centerxy[1]));
            float rad = (float)numRadius.Value;
            var initpt = new PointF(center.X, center.Y - rad);
            //initpt = RotatePoint(initpt, center, (float) numStartAngle.Value);
            float sweep = (float)numArcSweep.Value;
            int numpts = (int)numCircularPoints.Value;
            float increment = sweep / numpts;
            float curarc = (float)numStartAngle.Value;
            for (int i = 0; i < numpts; ++i) {
                Points.Add(new PointInfo(RotatePoint(initpt, center, curarc), Color.Black));
                curarc += increment;
                if (numSpiralStep.Value != 0) initpt = new PointF(initpt.X, initpt.Y + (float)numSpiralStep.Value);
            }
            lblPointCount.Text = "Points created: " + Points.Count;
            lblPointCount.Left = btnCancel.Right - lblPointCount.Width;
        }
        private void CreateImagePoints() {
            var fname = PersistentValues.optString("SelectedImageFile");
            if (string.IsNullOrEmpty(fname) || !File.Exists(fname)) {
                MessageBox.Show(string.IsNullOrEmpty(fname) ? "No file specified." : "File does not exist.", "Error");
                return;
            }
            try {
                using (var bmp = new Bitmap(PersistentValues.optString("SelectedImageFile"))) {
                    double xratio = (_fieldSize.Width - _margin * 2) / (double)(bmp.Width - 1);
                    double yratio = (_fieldSize.Height - _margin * 2) / (double)(bmp.Height - 1);
                    for (int y = 0; y < bmp.Height; ++y) {
                        for (int x = 0; x < bmp.Width; ++x) {
                            var c = bmp.GetPixel(x, y);
                            if (c.RGBEquals(Color.White) || c.A < 200) continue;
                            Points.Add(new PointInfo(new PointF((float)(x * xratio + _margin), (float)(y * yratio + _margin)), c));
                        }
                    }
                }
                lblPointCount.Text = "Points created: " + Points.Count;
                lblPointCount.Left = btnCancel.Right - lblPointCount.Width;
            }
            catch {
                MessageBox.Show("Unable to load image.", "Error");
            }
        }
        private void CreatePatternPoints() {
            var pat = cboxPattern.SelectedItem as Pattern;
            // scale to the specified size.
            var hwxy = txtPatternSize.Text.Split(',');
            var size = new SizeF(float.Parse(hwxy[0]), float.Parse(hwxy[1]));
            double scaleX = (double)size.Width / pat.Size.Width;
            double scaleY = (double)size.Height / pat.Size.Height;
            var points = new Point[pat.Points.Length];
            Array.Copy(pat.Points, points, pat.Points.Length);
            for (int i = 0; i < points.Length; ++i) {
                points[i] = new Point((int)Math.Round(points[i].X * scaleX), (int)Math.Round(points[i].Y * scaleY));
            }
            // calculate the total length of the path
            double totalLen = 0;
            for (var i = 0; i < points.Length; i++) {
                var p1 = points[i];
                var p2 = i == points.Length - 1 ? points[0] : points[i + 1];
                totalLen += Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
            }
            // divide the total length by the number of points.
            double spacer = totalLen / (int)numPatternPoints.Value;
            // Create the points.
            for (int i = 0; i < points.Length; ++i) {
                var p1 = points[i];
                var p2 = i == points.Length - 1 ? points[0] : points[i + 1];
                var dX = (double)(p2.X - p1.X) / (int)numPatternPoints.Value;
                var dY = (double)(p2.Y - p1.Y) / (int)numPatternPoints.Value;
                var curX = (double)p1.X;
                var curY = (double)p1.Y;
                for (int j = 0; j < (int)numPatternPoints.Value; ++j) {
                    Points.Add(new PointInfo(new PointF((float)curX, (float)curY), Color.Black));
                    curX += dX;
                    curY += dY;
                }
            }
            lblPointCount.Text = "Points created: " + Points.Count;
            lblPointCount.Left = btnCancel.Right - lblPointCount.Width;
        }
        private void CreateRectPoints() {
            var topleftxy = txtTopLeft.Text.Split(',');
            var topleft = new PointF(float.Parse(topleftxy[0]), float.Parse(topleftxy[1]));
            var hwxy = txtRectSize.Text.Split(',');
            var size = new SizeF(float.Parse(hwxy[0]), float.Parse(hwxy[1]));
            int perside = (int)numPointsPerSide.Value;
            float interval;
            if (chkTop.Checked) {
                interval = size.Width / (perside - 1);
                for (int i = 0; i < perside; ++i) {
                    Points.Add(new PointInfo(new PointF(topleft.X + interval * i - (i == perside - 1 ? 1 : 0), topleft.Y), Color.Black));
                }
            }
            if (chkBottom.Checked) {
                interval = size.Width / (perside - 1);
                for (int i = 0; i < perside; ++i) {
                    Points.Add(new PointInfo(new PointF(topleft.X + interval * i - (i == perside - 1 ? 1 : 0), topleft.Y + size.Height - 1), Color.Black));
                }
            }
            if (chkLeft.Checked) {
                interval = size.Height / (perside - 1);
                for (int i = 0; i < perside; ++i) {
                    if (i == 0 && chkTop.Checked) continue;
                    if (i == perside - 1 && chkBottom.Checked) continue;
                    Points.Add(new PointInfo(new PointF(topleft.X, topleft.Y + interval * i - (i == perside - 1 ? 1 : 0)), Color.Black));
                }
            }
            if (chkRight.Checked) {
                interval = size.Height / (perside - 1);
                for (int i = 0; i < perside; ++i) {
                    if (i == 0 && chkTop.Checked) continue;
                    if (i == perside - 1 && chkBottom.Checked) continue;
                    Points.Add(new PointInfo(new PointF(topleft.X + size.Width - 1, topleft.Y + interval * i - (i == perside - 1 ? 1 : 0)), Color.Black));
                }
            }
            lblPointCount.Text = "Points created: " + Points.Count;
            lblPointCount.Left = btnCancel.Right - lblPointCount.Width;
        }
        private void LoadPersistentValues() {
            numStartAngle.Value = PersistentValues.optInt("StartAngle", (int)numStartAngle.Value);
            numCircularPoints.Value = PersistentValues.optInt("CircularPoints", (int)numCircularPoints.Value);
            numArcSweep.Value = PersistentValues.optInt("ArcSweep", (int)numArcSweep.Value);
            txtCenter.Text = PersistentValues.optString("CircularCenter", txtCenter.Text);
            numRadius.Value = PersistentValues.optInt("Radius", (int)numRadius.Value);
            numSpiralStep.Value = PersistentValues.optInt("SpiralStep", (int)numSpiralStep.Value);
            numPointsPerSide.Value = PersistentValues.optInt("PointsPerSide", (int)numPointsPerSide.Value);
            txtTopLeft.Text = PersistentValues.optString("TopLeft", txtTopLeft.Text);
            txtRectSize.Text = PersistentValues.optString("RectSize", txtRectSize.Text);
            chkTop.Checked = PersistentValues.optBoolean("TopCheck", chkTop.Checked);
            chkBottom.Checked = PersistentValues.optBoolean("BottomCheck", chkBottom.Checked);
            chkLeft.Checked = PersistentValues.optBoolean("LeftCheck", chkLeft.Checked);
            chkRight.Checked = PersistentValues.optBoolean("RightCheck", chkRight.Checked);
            cboxPattern.SelectedIndex = PersistentValues.optInt("Pattern", cboxPattern.SelectedIndex);
            numPatternPoints.Value = PersistentValues.optInt("PatternPoints", (int)numPatternPoints.Value);
            txtPatternCenter.Text = PersistentValues.optString("PatternCenter", txtPatternCenter.Text);
            txtPatternSize.Text = PersistentValues.optString("PatternSize", txtPatternSize.Text);
            cboxImageFile.Items.Clear();
            var list = new List<ImageFileInfo>();
            foreach (string fname in PersistentValues.optJSONArray("RecentImageFiles") ?? new JSONArray()) {
                list.Add(new ImageFileInfo(fname));
            }
            cboxImageFile.Items.AddRange(list.ToArray<object>());
            cboxImageFile.SelectedIndex = list.FindIndex(i => i.Path == PersistentValues.optString("SelectedImageFile"));
            chkAddNewPoints.Checked = PersistentValues.optBoolean("AddToExisting");
            tabControl.SelectedIndex = PersistentValues.optInt("SelectedTab");
        }
        /// <summary>
        ///     Rotates one point arount another one
        /// </summary>
        public static PointF RotatePoint(PointF pointToRotate, PointF centerPoint, float angleInDegrees) {
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
        private readonly Size _fieldSize;
        private readonly int _margin;
        private Pattern[] _patterns = {
                                          new Pattern("Star",
                                                      new[] {
                                                                new Point(146, 0),
                                                                new Point(110, 110),
                                                                new Point(0, 112),
                                                                new Point(88, 180),
                                                                new Point(57, 289),
                                                                new Point(146, 226),
                                                                new Point(235, 289),
                                                                new Point(204, 180),
                                                                new Point(292, 112),
                                                                new Point(182, 110)
                                                            }),
                                      };
        public class ImageFileInfo {
            public ImageFileInfo(string path) {
                Path = path;
                Filename = System.IO.Path.GetFileName(path);
            }
            public override bool Equals(object obj) {
                if (!(obj is ImageFileInfo)) return false;
                return this.Equals((ImageFileInfo)obj);
            }
            public bool Equals(ImageFileInfo obj) {
                return string.Compare(obj.Path, Path, StringComparison.OrdinalIgnoreCase) == 0;
            }
            public override int GetHashCode() {
                unchecked {
                    return ((Path != null ? Path.GetHashCode() : 0) * 397) ^ (Filename != null ? Filename.GetHashCode() : 0);
                }
            }
            public override string ToString() {
                return Filename;
            }
            public readonly string Filename;
            public readonly string Path;
        }
        private class Pattern {
            public Pattern(string name, Point[] points) {
                Name = name;
                int minX = points.Min(p => p.X);
                int minY = points.Min(p => p.Y);
                for (int i = 0; i < points.Length; ++i) {
                    points[i] = new Point(points[i].X - minX, points[i].Y - minY);
                }
                Points = points;
                int maxX = points.Max(p => p.X);
                int maxY = points.Max(p => p.Y);
                Size = new Size(maxX, maxY);
            }
            public string Name { get; }
            public Point[] Points { get; }
            public Size Size { get; }
            public override string ToString() => Name;
        }
    }
}