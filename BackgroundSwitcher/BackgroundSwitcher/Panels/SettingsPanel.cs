using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace BackgroundSwitcher.Panels {
    public partial class SettingsPanel : MyUserControl {
        public SettingsPanel() {
            InitializeComponent();
        }
        private void numBorderWidth_ValueChanged(object sender, EventArgs e) {
            _settings.Margin = (int)numBorderWidth.Value;
            File.WriteAllText(Path.Combine(_dataPath, "Settings.json"), _settings.ToString(true));
        }
        private void numMinShowInterval_ValueChanged(object sender, EventArgs e) {
            _settings.MinShowIntervalDays = (int)numMinShowInterval.Value;
            File.WriteAllText(Path.Combine(_dataPath, "Settings.json"), _settings.ToString(true));
        }
        private void txtImageExtensions_TextChanged(object sender, EventArgs e) {
            if (txtImageExtensions.Text.Trim() == "") {
                txtImageExtensions.Text = "JPG, PNG";
                return;
            }
            var vals = txtImageExtensions.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();
            _settings.ImageExtensions = vals;
            File.WriteAllText(Path.Combine(_dataPath, "Settings.json"), _settings.ToString(true));
        }
        private void txtMinSize_TextChanged(object sender, EventArgs e) {
            if (txtMinSize.Text.Trim() == "") {
                txtMinSize.Text = "0, 0";
                return;
            }
            var vals = txtMinSize.Text.Split(',');
            if (vals.Length != 2) {
                InvokeShowMessage(Color.Red, "Minimum size must include height and width.");
                return;
            }
            int x, y;
            try {
                x = int.Parse(vals[0]);
                y = int.Parse(vals[1]);
            }
            catch {
                InvokeShowMessage(Color.Red, "Failed to parse minimum size values.  Must be in the form \"X, Y\"");
                return;
            }
            _settings.MinSourceImageSize = new Size(x, y);
            File.WriteAllText(Path.Combine(_dataPath, "Settings.json"), _settings.ToString(true));
        }
        public override void SetDataPath(string path) {
            base.SetDataPath(path);
            txtMinSize.Text = _settings.MinSourceImageSize.Width + ", " + _settings.MinSourceImageSize.Height;
            txtImageExtensions.Text = string.Join(", ", _settings.ImageExtensions);
            numBorderWidth.Value = _settings.Margin;
            numMinShowInterval.Value = _settings.MinShowIntervalDays;
        }
    }
}