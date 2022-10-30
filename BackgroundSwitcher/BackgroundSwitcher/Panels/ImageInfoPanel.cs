using System;
using System.Drawing;
using System.IO;

namespace BackgroundSwitcher.Panels {
    public partial class ImageInfoPanel : MyUserControl {
        public ImageInfoPanel() {
            InitializeComponent();
            _spacer = panelUnderPath.Top - lblPath.Bottom;
        }
        public bool WatchMouse {
            get { return chkWatchMouse.Checked; }
            set { chkWatchMouse.Checked = value; }
        }
        public event EventHandler GoToFile;
        public event EventHandler NeverShow;
        public event EventHandler OpenFocusRectEditor;
        public event EventHandler OpenImage;
        public event EventHandler WatchMouseChanged;
        private void btnFocusRectEdit_Click(object sender, EventArgs e) {
            OpenFocusRectEditor?.Invoke(this, EventArgs.Empty);
        }
        private void btnGoToFile_Click(object sender, EventArgs e) {
            GoToFile?.Invoke(this, EventArgs.Empty);
        }
        private void btnNeverShow_Click(object sender, EventArgs e) {
            NeverShow?.Invoke(this, EventArgs.Empty);
        }
        private void btnOpenImage_Click(object sender, EventArgs e) {
            OpenImage?.Invoke(this, EventArgs.Empty);
        }
        private void chkWatchMouse_CheckedChanged(object sender, EventArgs e) {
            _settings.ClickOutsideWindow = chkWatchMouse.Checked;
            SaveSettings();
            WatchMouseChanged?.Invoke(this, EventArgs.Empty);
        }
        private void lblPath_SizeChanged(object sender, EventArgs e) {
            if (_spacer == 0) return;
            panelUnderPath.Location = new Point(0, lblPath.Bottom + _spacer);
            panelUnderPath.Size = new Size(Width, Height - panelUnderPath.Top);
        }
        public void FillValues(JSONImageInfo info) {
            if (info == null) {
                lblPath.Text = "No file";
                lblOrigSize.Text = lblDispSize.Text = lblShowCount.Text = lblRecentShows.Text = "";
                pboxImage.BackgroundImage = null;
                return;
            }
            lblPath.Text = info.Path;
            lblOrigSize.Text = info.Size.Width + ", " + info.Size.Height + " (" + info.Ratio.ToString("F3") + ")";
            lblDispSize.Text = info.DestRect.Width + ", " + info.DestRect.Height + " (" + info.DestRatio.ToString("F3") + ")";
            lblShowCount.Text = info.ShowCount.ToString();
            DateTime prevdt = DateTime.MinValue;
            string s = "";
            string newline = "";
            foreach (var dt in info.RecentShows) {
                s += newline + dt.ToString("g");
                if ((dt - prevdt).TotalDays < _settings.MinShowIntervalDays) s += " (too soon!)";
                prevdt = dt;
                newline = Environment.NewLine;
            }
            lblRecentShows.Text = s;
            Text = Path.GetFileName(info.Path);
            if (!File.Exists(info.Path)) {
                InvokeShowMessage(Color.Red, "File does not exist: " + info.Path);
            }
            else {
                //FocusRectEditor?.SetImage(info.Path);
                var bmp = new Bitmap(info.Path);
                using (var gfx = Graphics.FromImage(bmp)) {
                    if (info.has("FocusRect")) {
                        var frect = info.FocusRect;
                        gfx.DrawRectangle(new Pen(Color.White, LineWidth * 3), frect.X, frect.Y, frect.Width, frect.Height);
                        //gfx.DrawRectangle(new Pen(Color.White, LineWidth), frect.X + LineWidth, frect.Y + LineWidth, frect.Width - LineWidth * 2, frect.Height - LineWidth * 2);
                        gfx.DrawRectangle(new Pen(Color.Red, LineWidth), frect.X, frect.Y, frect.Width, frect.Height);
                    }
                    if (info.has("RectShown")) {
                        var srect = info.RectShown;
                        double ratio = info.Size.Height / (double)info.RectShown.Height;
                        if (info.RectShown.Width * ratio > info.Size.Width) ratio = info.Size.Width / (double)info.RectShown.Width;
                        var srectf = new RectangleF(info.RectShown.X * (float)ratio, info.RectShown.Y * (float)ratio, info.RectShown.Width * (float)ratio, info.RectShown.Height * (float)ratio);
                        gfx.DrawRectangle(new Pen(Color.LimeGreen, LineWidth * 3), srectf.X, srectf.Y, srectf.Width, srectf.Height);
                        gfx.DrawRectangle(new Pen(Color.LawnGreen, LineWidth), srectf.X, srectf.Y, srectf.Width, srectf.Height);
                    }
                }

                pboxImage.BackgroundImage = bmp;
            }
        }
        public override void SetDataPath(string path) {
            base.SetDataPath(path);
            chkWatchMouse.Checked = _settings.ClickOutsideWindow;
        }
        protected override void OnSizeChanged(EventArgs e) {
            base.OnSizeChanged(e);
            lblPath.MaximumSize = new Size(Width - lblPath.Left * 2, 10000);
        }
        private readonly int _spacer;
        //public FocusRectEditor FocusRectEditor;
        public int LineWidth = 4;
    }
}