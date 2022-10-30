using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BackgroundSwitcher.Panels {
    public partial class MyUserControl : UserControl {
        public MyUserControl() {
            InitializeComponent();
        }
        public bool InDesigner {
            get {
                //return true;
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return true;
                using (Process process = Process.GetCurrentProcess()) {
                    if (process.ProcessName == "devenv") return true;
                }
                return false;
            }
        }
        public virtual Size TargetSize { get; set; }
        public event EventHandler<MessageInfo> ShowMessage;
        public void InvokeShowMessage(Color c, string msg) {
            ShowMessage?.Invoke(this, new MessageInfo(c, msg));
        }
        public virtual void SetDataPath(string path) {
            _dataPath = path;
            try {
                _settings = new JSONSettings(File.ReadAllText(Path.Combine(_dataPath, "Settings.json")));
            }
            catch (Exception ex) {
                ShowMessage?.Invoke(this, new MessageInfo(Color.Red, "Error loading Settings.json: " + ex.Message));
            }
        }
        protected override void CreateHandle() {
            base.CreateHandle();
            if (InDesigner) {
                lblTypeName.Text = GetType().Name;
                lblTypeName.Location = new Point(0, 0);
                //lblTypeName.AutoSize = false;
                //lblTypeName.Dock = DockStyle.Fill;
                lblTypeName.Visible = true;
                lblTypeName.BringToFront();
                BorderStyle = BorderStyle.FixedSingle;
            }
            else {
                BorderStyle = BorderStyle.None;
                lblTypeName.SendToBack();
            }
        }
        protected void SaveSettings() {
            File.WriteAllText(Path.Combine(_dataPath, "Settings.json"), _settings.ToString(true));
        }
        protected string _dataPath;
        protected JSONSettings _settings;
    }
}