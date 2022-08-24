using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using JSON;

namespace ChaosGame {
    public partial class MainWindow : Form {
        public MainWindow() {
            InitializeComponent();
            string datapath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "PointGame");
            _settingsPath = Path.Combine(datapath, "Settings.json");
            Directory.CreateDirectory(datapath);
            if (File.Exists(_settingsPath)) {
                _settings = new JSONObject(File.ReadAllText(_settingsPath));
                _operSettingsPersistValues = _settings.optJSONObject("OperationalSettings");
                _genPtsPersistValues = _settings.optJSONObject("GeneratePointsSettings");
                numSize.Value = _settings.optInt("Size", (int)numSize.Value);
                numMargin.Value = _settings.optInt("Margin", (int)numMargin.Value);
            }
            else _settings = new JSONObject();
            UpdateOperationalSettings();
            if (_canvas == null) InitCanvas();
        }
        private void btnAlgorithmicSettings_Click(object sender, EventArgs e) {
            using (var form = new AlgorithmicSettings(_operSettingsPersistValues)) {
                if (form.ShowDialog(this) == DialogResult.OK) {
                    _operSettingsPersistValues = form.PersistentValues;
                    _settings.put("OperationalSettings", _operSettingsPersistValues);
                    File.WriteAllText(_settingsPath, _settings.ToString(true));
                    UpdateOperationalSettings();
                    pointGamePanel.MaxIterations = (ulong)(_operSettingsPersistValues.optBoolean("MaxIterationsEnabled") ? _operSettingsPersistValues.optInt("MaxIterations") : 0);
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e) {
            pointGamePanel.Clear();
            //InitCanvas();
            UpdatePointCount();
        }
        private void btnGo_Click(object sender, EventArgs e) {
            switch (btnGo.Text) {
                case "Go":
                    btnGo.Text = "Stop";
                    //pointGamePanel.BackgroundImage = _canvas;
                    if (_operSettingsPersistValues?.optBoolean("RandomizeBlack", true) ?? true) pointGamePanel.RandomizeBlackPoints();
                    //LoadImage();
                    //pointGamePanel.TravelDist = float.Parse(txtTravelDist.Text);
                    var thread = new Thread(pointGamePanel.ThreadProc);
                    pointGamePanel.Running = true;
                    thread.Start();
                    _startTime = DateTime.Now;
                    timer.Start();
                    break;
                case "Stop":
                    btnGo.Text = "Go";
                    pointGamePanel.Running = false;
                    timer.Stop();
                    break;
            }
        }
        private void btnLoad_Click(object sender, EventArgs e) {
            using (var dlg = new OpenFileDialog {
                                                    Title = "Load saved configuration",
                                                    InitialDirectory = Path.Combine(Path.GetDirectoryName(_settingsPath), "Saved configs"),
                                                    Filter = "Saved configurations|*.json",
                                                    DefaultExt = "json"
                                                }) {
                if (dlg.ShowDialog(this) == DialogResult.OK) {
                    var json = new JSONObject(File.ReadAllText(dlg.FileName));
                    _settings = json.getJSONObject("Settings");
                    _operSettingsPersistValues = _settings.optJSONObject("OperationalSettings");
                    _genPtsPersistValues = _settings.optJSONObject("GeneratePointsSettings");
                    numSize.Value = json.getInt("Size");
                    numMargin.Value = json.getInt("Margin");
                    UpdateOperationalSettings();
                    InitCanvas();
                    var parray = json.getJSONObject("Panel").getJSONArray("Points");
                    foreach (JSONObject p in parray) {
                        _canvas.SetPixel((int)Math.Round(p.getDouble("X")), (int)Math.Round(p.getDouble("Y")), Color.FromArgb(p.getInt("R"), p.getInt("G"), p.getInt("B")));
                    }
                    pointGamePanel.BackgroundImage = _canvas;
                    UpdatePointCount();
                }
            }
        }
        private void btnPlot_Click(object sender, EventArgs e) {
            using (var form = new GeneratePoints(pointGamePanel.FieldSize, (int)numMargin.Value, _genPtsPersistValues)) {
                if (form.ShowDialog(this) == DialogResult.OK) {
                    _genPtsPersistValues = form.PersistentValues;
                    _settings.put("GeneratePointsSettings", _genPtsPersistValues);
                    File.WriteAllText(_settingsPath, _settings.ToString(true));
                    if (!_genPtsPersistValues.optBoolean("AddToExisting")) {
                        pointGamePanel.Clear();
                        InitCanvas();
                    }
                    foreach (var pi in form.Points) {
                        Point p = new Point((int)Math.Round(pi.P.X), (int)Math.Round(pi.P.Y));
                        int x = Math.Min(pointGamePanel.FieldSize.Width - 1, Math.Max(0, p.X));
                        int y = Math.Min(pointGamePanel.FieldSize.Height - 1, Math.Max(0, p.Y));
                        //if (p.X < 0 || p.Y < 0 || p.X >= pointGamePanel.FieldSize.Width || p.Y >= pointGamePanel.FieldSize.Height) continue;
                        _canvas.SetPixel(x, y, pi.C);
                    }
                    pointGamePanel.BackgroundImage = _canvas;
                    if (_operSettingsPersistValues?.optBoolean("RandomizeBlack", true) ?? true) pointGamePanel.RandomizeBlackPoints();
                    UpdatePointCount();
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e) {
            using (var dlg = new SaveFileDialog {
                                                    Title = "Save configuration",
                                                    InitialDirectory = Path.Combine(Path.GetDirectoryName(_settingsPath), "Saved configs"),
                                                    DefaultExt = "json"
                                                }) {
                if (dlg.ShowDialog(this) == DialogResult.OK) {
                    var json = new JSONObject();
                    json.put("Settings", _settings);
                    json.put("Size", (int)numSize.Value);
                    json.put("Margin", (int)numMargin.Value);
                    json.put("Panel", pointGamePanel.ToJSON());
                    File.WriteAllText(dlg.FileName, json.ToString(true));
                }
            }
        }
        private void chkHilitePoints_CheckedChanged(object sender, EventArgs e) {
            _settings.put("HilitePoints", chkHilitePoints.Checked);
            File.WriteAllText(_settingsPath, _settings.ToString(true));
            pointGamePanel.HilitePoints = chkHilitePoints.Checked;
            pointGamePanel.Invalidate();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            pointGamePanel.Running = false;
        }
        private void numMargin_ValueChanged(object sender, EventArgs e) {
            _settings.put("Margin", (int)numMargin.Value);
            File.WriteAllText(_settingsPath, _settings.ToString(true));
            InitCanvas();
        }
        private void numSize_ValueChanged(object sender, EventArgs e) {
            _settings.put("Size", (int)numSize.Value);
            File.WriteAllText(_settingsPath, _settings.ToString(true));
            InitCanvas();
        }
        private void pointGamePanel_MouseUp(object sender, MouseEventArgs e) {
            //if (e.Button == MouseButtons.Right) {
            //    var cmenu = new ContextMenuStrip();
            //    foreach (ToolStripItem item in contextMenu.Items) 
            //        cmenu.Items.Add(new ToolStripMenuItem(item.Text, null, (o, args) => { ((ToolStripItem)((ToolStripItem)o).Tag).PerformClick(); }) { Tag = item });
            //    if (pointGamePanel.ContextMenuStrip != null) {
            //        cmenu.Items.Add(new ToolStripSeparator());
            //        foreach (ToolStripItem item in pointGamePanel.ContextMenuStrip.Items) 
            //            cmenu.Items.Add(new ToolStripMenuItem(item.Text, null, (o, args) => { ((ToolStripItem)((ToolStripItem)o).Tag).PerformClick(); }) { Tag = item });
            //    }
            //    cmenu.Show(pointGamePanel, e.Location);
            //}
        }
        private void pointGamePanel_PointsCreated(object sender, EventArgs e) {
            btnGo.Enabled = pointGamePanel.Count > 0;
        }
        private void timer_Tick(object sender, EventArgs e) {
            double itsPerSec = pointGamePanel.Iterations / (DateTime.Now - _startTime).TotalMilliseconds;
            lblIterations.Text = pointGamePanel.Iterations + " (" + itsPerSec.ToString("F2") + ")";
            pointGamePanel.Invalidate();
        }
        private void timerHideDimensions_Tick(object sender, EventArgs e) {
            if (_hideDimensions != null && DateTime.Now > _hideDimensions.Value) {
                _hideDimensions = null;
                lblDimensions.Visible = false;
                timerHideDimensions.Enabled = false;
            }
        }
        protected override void OnSizeChanged(EventArgs e) {
            base.OnSizeChanged(e);
            lblDimensions.Text = pointGamePanel.Width + ", " + pointGamePanel.Height;
            lblDimensions.Visible = true;
            _hideDimensions = DateTime.Now.AddSeconds(5);
            timerHideDimensions.Enabled = true;
        }
        private void InitCanvas() {
            _canvas = new Bitmap((int)(numSize.Value + numMargin.Value * 2), (int)(numSize.Value + numMargin.Value * 2));
            using (var gfx = Graphics.FromImage(_canvas)) {
                gfx.FillRectangle(Brushes.White, 0, 0, _canvas.Width, _canvas.Height);
            }
            pointGamePanel.BackgroundImage = _canvas;
            UpdatePointCount();
        }
        private void UpdateOperationalSettings() {
            bool pickone = _operSettingsPersistValues?.optBoolean("OnePoint", true) ?? true;
            string method = pickone
                ? AlgorithmicSettings.PickOnePointOptions[_operSettingsPersistValues?.optInt("OnePointMethod") ?? 0]
                : AlgorithmicSettings.PickTwoPointsOptions[_operSettingsPersistValues.optInt("TwoPointsMethod")];
            string movement = pickone
                ? AlgorithmicSettings.OnePointMovementOptions[_operSettingsPersistValues?.optInt("OnePointMovement") ?? 0]
                : AlgorithmicSettings.TwoPointMovementOptions[_operSettingsPersistValues.optInt("TwoPointMovement")];
            lblOperSettings.Text = (pickone ? "One point, " : "Two points, ") + method + ", " + movement + Environment.NewLine +
                                   "Dist.: " + (_operSettingsPersistValues?.optString("TravelDist", "0.5") ?? "0.5") +
                                   (_operSettingsPersistValues?.optBoolean("RandomizeBlack", true) ?? true ? ", randomize black" : ", keep black") +
                                   (_operSettingsPersistValues?.optBoolean("MixColors", true) ?? true ? ", mix colors" : ", no mixing") +
                                   (_operSettingsPersistValues?.optBoolean("DrawLines") ?? false ? ", draw lines" : ", draw points only");
            pointGamePanel.PtSelection = pickone ? PointGamePanel.PointSelection.PickOne : PointGamePanel.PointSelection.PickTwo;
            pointGamePanel.PickOneSelection = (PointGamePanel.OnePointSelection)(_operSettingsPersistValues?.optInt("OnePointMethod") ?? 0);
            pointGamePanel.PickOneMovement = (PointGamePanel.OnePointMovement)(_operSettingsPersistValues?.optInt("OnePointMovement") ?? 0);
            pointGamePanel.PickTwoSelection = (PointGamePanel.TwoPointSelection)(_operSettingsPersistValues?.optInt("TwoPointMethod") ?? 0);
            pointGamePanel.PickTwoMovement = (PointGamePanel.TwoPointMovement)(_operSettingsPersistValues?.optInt("TwoPointMovement") ?? 0);
            pointGamePanel.MixColors = _operSettingsPersistValues?.optBoolean("MixColors", true) ?? true;
            pointGamePanel.DrawLines = _operSettingsPersistValues?.optBoolean("DrawLines") ?? false;
            pointGamePanel.TravelDist = float.Parse(_operSettingsPersistValues?.optString("TravelDist", "0.5") ?? "0.5");
            switch (_operSettingsPersistValues?.optString("DistanceMeaning")) {
                default:
                case "Fraction of point dist":
                    pointGamePanel.DistMeaning = PointGamePanel.DistanceMeaning.FractionDistToTarget;
                    break;
                case "Fraction of max dist":
                    pointGamePanel.DistMeaning = PointGamePanel.DistanceMeaning.FractionMaxDist;
                    break;
                case "Fraction of min dist":
                    pointGamePanel.DistMeaning = PointGamePanel.DistanceMeaning.FractionMinDist;
                    break;
                case "Constant dist":
                    pointGamePanel.DistMeaning = PointGamePanel.DistanceMeaning.ConstantDist;
                    break;
                //case "Combo dist":
                //    pointGamePanel.DistMeaning = PointGamePanel.DistanceMeaning.ComboDist;
                //    break;
            }
            bool comboMixEnabled = _operSettingsPersistValues?.optBoolean("ComboMixEnabled") ?? false;
            pointGamePanel.ComboMix = comboMixEnabled ? _operSettingsPersistValues?.optInt("ComboMix") ?? 0 : -1;
            pointGamePanel.PickRadius = double.Parse(_operSettingsPersistValues?.optString("PointPickRadius", "100") ?? "100");
            pointGamePanel.PickN = int.Parse(_operSettingsPersistValues?.optString("PickN", "2") ?? "2");
            pointGamePanel.MoveN = int.Parse(_operSettingsPersistValues?.optString("MoveN", "2") ?? "2");
            pointGamePanel.IncludeCurPutInMoveCalc = _operSettingsPersistValues?.optBoolean("IncludeCurPtInMoveCalc") ?? false;
            pointGamePanel.MaxIterations = (ulong)(_operSettingsPersistValues?.optBoolean("MaxInterationsEnabled") ?? false ? 0 : _operSettingsPersistValues?.optInt("MaxInterations") ?? 0);
            if (!(_operSettingsPersistValues?.optBoolean("MaxInterationsEnabled") ?? false)) pointGamePanel.MaxIterations = 0;
            pointGamePanel.ConstantDist = float.Parse(_operSettingsPersistValues?.optString("ConstantDist", "100") ?? "100");
            pointGamePanel.CircularPath = _operSettingsPersistValues?.optBoolean("CircularPath") ?? false;
        }
        private void UpdatePointCount() {
            lblPoints.Text = "Points: " + pointGamePanel.Count;
        }
        private Bitmap _canvas;
        private JSONObject _genPtsPersistValues;
        private DateTime? _hideDimensions = null;
        private JSONObject _operSettingsPersistValues;
        private JSONObject _settings;
        private readonly string _settingsPath;
        private DateTime _startTime;
    }
}