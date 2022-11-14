using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using BackgroundSwitcher.Panels;
using JSON;

namespace BackgroundSwitcher {
    public sealed partial class MainForm : Form {
        public MainForm(string datapath) {
            _datapath = datapath;
            InitializeComponent();
            _panels = new MyUserControl[] { panelImageInfo, panelSettings, panelFolders, panelFocusRects };
            multiSliderPanel.Main = panelImageInfo;
            foreach (var p in _panels) {
                p.SetDataPath(_datapath);
                if (p == panelImageInfo) continue;
                multiSliderPanel.AddSlider(p, true);
            }
            multiSliderPanel.Height = ClientRectangle.Height - multiSliderPanel.Top;
        }
        public MainForm(JSONImageInfo[] infoarray, string datapath, JSONSettings settings) : this(datapath) {
            _settings = settings;
            Location = MousePosition;
            _info = infoarray.FirstOrDefault(i => i.DestRect.Contains(Location));
            _infoArray = infoarray;
            if (_info != null) FillValues(_info);
        }
        public int LoadingProgMax {
            get => panelFocusRects.ProgbarMax;
            set => panelFocusRects.ProgbarMax = value;
        }
        public int LoadingProgValue {
            get => panelFocusRects.ProgbarValue;
            set => panelFocusRects.ProgbarValue = value;
        }
        //public FocusRectEditor FocusRectEditor {
        //    get => panelImageInfo.FocusRectEditor;
        //    set {
        //        panelImageInfo.FocusRectEditor = value;
        //        if (_info != null) panelImageInfo.FocusRectEditor?.SetImage(_info.Path);
        //    }
        //}
        public event EventHandler<string> EditImage;
        public event EventHandler OpenFocusRectEditor;
        public event EventHandler<FocusRectsPanel.PrepFocusRectsPanelEventArgs> PrepFocusRectsPanel;
        private void btnFocusRectEdit_Click(object sender, EventArgs e) {
            ShowMessage(Color.Green, "Opening focus rect editor...");
            OpenFocusRectEditor?.Invoke(this, EventArgs.Empty);
        }
        private void btnGoToFile_Click(object sender, EventArgs e) {
            try {
                ShowMessage(Color.Green, "Starting explorer.exe...");
                Process.Start("explorer.exe", "/select," + _info.Path);
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
                ShowMessage(Color.Green, "Launching editor for this file...");
                EditImage?.Invoke(this, _info.Path);
            }
            catch (Exception ex) {
                ShowMessage(Color.Red, "Error: " + ex.Message);
            }
        }
        private void chkWatchMouseChanged(object sender, EventArgs e) {
            if (sender == panelImageInfo) panelSettings.ClickOutsideWindow = panelImageInfo.WatchMouse;
            else panelImageInfo.WatchMouse = panelSettings.ClickOutsideWindow;
        }
        private void MouseHook_OnMouseAction(object sender, MouseHook.MouseHookEventArgs e) {
            if (!panelImageInfo.WatchMouse || multiSliderPanel.Current != panelImageInfo ||
                new Rectangle(Location, Size).Contains(e.Data.pt.x, e.Data.pt.y)) // ||
                //FocusRectEditor != null && new Rectangle(FocusRectEditor.Location, FocusRectEditor.Size).Contains(e.Data.pt.x, e.Data.pt.y)) return;
                return;
            if (e.Type == MouseHook.MouseMessages.WM_LBUTTONDOWN) btnOpenImage_Click(null, null);
            else if (e.Type == MouseHook.MouseMessages.WM_RBUTTONDOWN) btnGoToFile_Click(null, null);
        }
        private void multiSliderPanel_SizeChanged(object sender, EventArgs e) {
            if (_panels == null) return;
            foreach (var panel in _panels) {
                panel.TargetSize = multiSliderPanel.Size;
            }
        }
        private void panel_ShowMessage(object sender, MessageInfo e) {
            ShowMessage(e.Color, e.Message);
        }
        private void panelFocusRects_EditImage(object sender, string path) {
            if (_openingImage || string.IsNullOrEmpty(path) || !File.Exists(path)) return;
            _openingImage = true;
            try {
                ShowMessage(Color.Green, "Launching editor for image...");
                EditImage?.Invoke(this, path);
            }
            catch (Exception ex) {
                ShowMessage(Color.Red, "Error: " + ex.Message);
            }
        }
        private void panelFocusRects_PrepFocusRectsPanel(object sender, FocusRectsPanel.PrepFocusRectsPanelEventArgs e) {
            PrepFocusRectsPanel?.Invoke(sender, e);
        }
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e) {
            switch (tabControl.SelectedTab.Name) {
                case "pageImageInfo":
                    multiSliderPanel.SlideBack(true);
                    break;
                case "pageSettings":
                    multiSliderPanel.SlideTo(panelSettings);
                    break;
                case "pageFolders":
                    multiSliderPanel.SlideTo(panelFolders);
                    break;
                case "pageFocusRects":
                    multiSliderPanel.SlideTo(panelFocusRects);
                    break;
            }
        }
        private void tabControl_TabIndexChanged(object sender, EventArgs e) {
        }
        private void timer1_Tick(object sender, EventArgs e) {
            var m = MousePosition;
            var now = DateTime.Now;
            // Clear the message after 10 seconds.
            if (lblMessage.Visible && (now - _messageShown).TotalSeconds > 10) {
                ShowMessage(Color.Black, "");
            }
            if ((multiSliderPanel.Current == panelImageInfo || multiSliderPanel.Current == panelFocusRects)) {
                if (Keyboard.IsKeyDown(Key.Escape) && panelImageInfo.WatchMouse) {
                    Close();
                }
                // Update the UI every 500 ms.
                if ((now - _lastUIUpdate).TotalMilliseconds > 500) {
                    _openingImage = false; // clear this every 500 ms.
                    _lastUIUpdate = now;
                    //lblMouseCoords.Text = $"({m.X}, {m.Y})";
                    //lblMouseCoords.Left = Width - (lblMouseCoords.Width + 30);
                    if (panelImageInfo.WatchMouse && !new Rectangle(Location, Size).Contains(m)) { // && (FocusRectEditor == null || !new Rectangle(FocusRectEditor.Location, FocusRectEditor.Size).Contains(m))) {
                        var info = GetInfo(m);
                        if (info != _info) {
                            _info = info;
                            FillValues(info);
                        }
                    }
                }
            }
        }
        protected override void OnClosing(CancelEventArgs e) {
            base.OnClosing(e);
            MouseHook.Stop();
            //FocusRectEditor?.Close();
        }
        protected override void OnResizeEnd(EventArgs e) {
            base.OnResizeEnd(e);
            panelFocusRects.OnResizeEnd(e);
        }
        protected override void OnShown(EventArgs e) {
            MouseHook.Start();
            MouseHook.MouseAction += MouseHook_OnMouseAction;
            base.OnShown(e);
            //TopMost = true;
            timer1.Start();
            //Capture = true;
        }
        //private void MouseHook_OnMouseAction(object sender, EventArgs e) {
        //    
        //}
        private void FillValues(JSONImageInfo info) {
            panelImageInfo.FillValues(info);
            panelFocusRects.SetImage(info?.Path);
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
            msg = msg.Trim();
            lblMessage.ForeColor = c;
            lblMessage.Text = msg;
            lblMessage.Visible = !string.IsNullOrEmpty(msg);
            _messageShown = DateTime.Now;
            if (string.IsNullOrEmpty(msg.Trim())) multiSliderPanel.Height = ClientRectangle.Height - multiSliderPanel.Top;
            else multiSliderPanel.Height = ClientRectangle.Height - (multiSliderPanel.Top + lblMessage.Height + 5);
        }
        private readonly string _datapath;
        private JSONImageInfo _info;
        private readonly JSONImageInfo[] _infoArray;
        private DateTime _lastUIUpdate = DateTime.MinValue;
        private DateTime _messageShown;
        private bool _openingImage;
        private MyUserControl[] _panels;
        private JSONSettings _settings;
    }
}