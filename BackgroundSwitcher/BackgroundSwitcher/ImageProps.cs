using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using JSON;

namespace BackgroundSwitcher {
    public sealed partial class ImageProps : Form {
        public ImageProps(string datapath) {
            _datapath = datapath;
            InitializeComponent();
        }
        public ImageProps(JSONImageInfo[] infoarray, string datapath, JSONSettings settings) : this(datapath) {
            _settings = settings;
            Location = MousePosition;
            _info = infoarray.FirstOrDefault(i => i.DestRect.Contains(Location));
            _infoArray = infoarray;
            if (_info != null) FillValues(_info);
        }
        public FocusRectEditor FocusRectEditor {
            get => _focusRectEditor;
            set {
                _focusRectEditor = value;
                if (_info != null) _focusRectEditor?.SetImage(_info.Path);
            }
        }
        public event EventHandler<string> EditImage;
        public event EventHandler OpenFocusRectEditor;
        private void btnFocusRectEdit_Click(object sender, EventArgs e) {
            OpenFocusRectEditor?.Invoke(this, EventArgs.Empty);
        }
        private void btnGoToFile_Click(object sender, EventArgs e) {
            try {
                Process.Start("explorer.exe", "/select," + _info.Path);
                ShowMessage(Color.Green, "Starting explorer.exe...");
            }
            catch (Exception ex) {
                ShowMessage(Color.Red, "Error: " + ex.Message);
            }
        }
        private void btnNeverShow_Click(object sender, EventArgs e) {
            try {
                string nevershowfname = Path.Combine(_datapath, "NeverShow.json");
                var nevershow = File.Exists(nevershowfname) ? new JSONArray(File.ReadAllText(nevershowfname)) : new JSONArray();
                nevershow.put(Path.GetFileNameWithoutExtension(_info.Path));
                File.WriteAllText(nevershowfname, nevershow.ToString(true, 4));
                ShowMessage(Color.Green, "Image added to never show list.");
            }
            catch (Exception ex) {
                ShowMessage(Color.Red, "Error: " + ex.Message);
            }
        }
        private void btnOpenImage_Click(object sender, EventArgs e) {
            if (_openingImage || _info == null) return;
            _openingImage = true;
            try {
                EditImage?.Invoke(this, _info.Path);
                ShowMessage(Color.Green, "Launching editor for this file...");
            }
            catch (Exception ex) {
                ShowMessage(Color.Red, "Error: " + ex.Message);
            }
        }
        private void MouseHook_OnMouseAction(object sender, MouseHook.MouseHookEventArgs e) {
            //ShowMessage(Color.Green, e.Type.ToString());
            if (new Rectangle(Location, Size).Contains(e.Data.pt.x, e.Data.pt.y) ||
                FocusRectEditor != null && new Rectangle(FocusRectEditor.Location, FocusRectEditor.Size).Contains(e.Data.pt.x, e.Data.pt.y)) return;
            //ShowMessage(Color.Green, "mouse action: " + MouseHook.MouseMessageToString(e.Type));
            if (e.Type == MouseHook.MouseMessages.WM_LBUTTONDOWN) btnOpenImage_Click(null, null);
            else if (e.Type == MouseHook.MouseMessages.WM_RBUTTONDOWN) btnGoToFile_Click(null, null);
        }
        private void timer1_Tick(object sender, EventArgs e) {
            var m = MousePosition;
            var now = DateTime.Now;
            if (Keyboard.IsKeyDown(Key.Escape)) {
                Close();
            }
            // Update the UI every 500 ms.
            if ((now - _lastUIUpdate).TotalMilliseconds > 500) {
                _openingImage = false; // clear this every 500 ms.
                _lastUIUpdate = now;
                lblMouseCoords.Text = $"({m.X}, {m.Y})";
                lblMouseCoords.Left = Width - (lblMouseCoords.Width + 30);
                if (FocusRectEditor == null || !new Rectangle(FocusRectEditor.Location, FocusRectEditor.Size).Contains(m)) {
                    var info = GetInfo(m);
                    if (info != _info) {
                        _info = info;
                        FillValues(info);
                    }
                }
            }
            // Clear the message after 10 seconds.
            if (lblMessage.Visible && (now - _messageShown).TotalSeconds > 10) {
                lblMessage.Visible = false;
            }
        }
        protected override void OnClosing(CancelEventArgs e) {
            base.OnClosing(e);
            MouseHook.Stop();
            FocusRectEditor?.Close();
        }
        protected override void OnShown(EventArgs e) {
            MouseHook.Start();
            MouseHook.MouseAction += MouseHook_OnMouseAction;
            base.OnShown(e);
            TopMost = true;
            timer1.Start();
            //Capture = true;
        }
        private JSONSettings _settings;
        //private void MouseHook_OnMouseAction(object sender, EventArgs e) {
        //    
        //}
        private void FillValues(JSONImageInfo info) {
            if (info == null) {
                lblValues.Text = "";
                Text = "No file";
                return;
            }
            string s = _info.Path + Environment.NewLine +
                       _info.Size.Width + ", " + _info.Size.Height + " (" + _info.Ratio.ToString("F3") + ")" + Environment.NewLine +
                       _info.DestRect.Width + ", " + _info.DestRect.Height + " (" + _info.DestRatio.ToString("F3") + ")" + Environment.NewLine +
                       _info.ShowCount;
            DateTime prevdt = DateTime.MinValue;
            foreach (var dt in _info.RecentShows) {
                s += Environment.NewLine + dt.ToString("g");
                if ((dt - prevdt).TotalDays < _settings.MinShowIntervalDays) s += " (too soon!)";
                prevdt = dt;
            }
            lblValues.Text = s;
            Text = Path.GetFileName(info.Path);
            if (!File.Exists(info.Path)) {
                ShowMessage(Color.Red, "File does not exist: " + info.Path);
            }
            else FocusRectEditor?.SetImage(info.Path);
        }
        private JSONImageInfo GetInfo(Point p) {
            foreach (var info in _infoArray) {
                if (info.DestRect.Contains(p)) {
                    return info;
                }
            }
            //ShowMessage(Color.Red, "No image found at mouse position.");
            return null;
        }
        private void ShowMessage(Color c, string msg) {
            lblMessage.ForeColor = c;
            lblMessage.Text = msg;
            lblMessage.Visible = true;
            _messageShown = DateTime.Now;
        }
        private readonly string _datapath;
        private FocusRectEditor _focusRectEditor;
        private JSONImageInfo _info;
        private readonly JSONImageInfo[] _infoArray;
        private DateTime _lastUIUpdate = DateTime.MinValue;
        private DateTime _messageShown;
        private bool _openingImage;
    }
}