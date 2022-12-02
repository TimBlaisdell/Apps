using System;
using System.Drawing;
using System.Windows.Forms;
using SingleLineDrawing.Properties;

namespace SingleLineDrawing {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            try {
                if (!string.IsNullOrEmpty(Settings.Default.TargetFile)) lineDrawer.TargetImage = new Bitmap(Settings.Default.TargetFile);
            }
            catch {
                // do nothing.
            }
            numMagnitude_ValueChanged(null, null);
        }
        private void btnStart_Click(object sender, EventArgs e) {
            switch (btnStart.Text) {
                case "Start":
                    lineDrawer.Start();
                    if (lineDrawer.Running) {
                        btnStart.Text = "Stop";
                        btnTargetImage.Enabled = false;
                    }
                    break;
                case "Stop":
                    lineDrawer.Stop();
                    if (!lineDrawer.Running) {
                        btnStart.Text = "Start";
                        btnTargetImage.Enabled = true;
                    }
                    break;
            }
        }
        private void btnTargetImage_Click(object sender, EventArgs e) {
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                lineDrawer.TargetImage = new Bitmap(openFileDialog.FileName);
                Settings.Default.TargetFile = openFileDialog.FileName;
                Settings.Default.Save();
            }
        }
        private void chkRotatingSequence_CheckedChanged(object sender, EventArgs e) {
            lineDrawer.CreateRotatingSequence = chkRotatingSequence.Checked;
            if (chkRotatingSequence.Checked) {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK) {
                    lineDrawer.RotatingSequenceFolder = folderBrowserDialog.SelectedPath;
                }
                else lineDrawer.CreateRotatingSequence = chkRotatingSequence.Checked = false;
            }
            numIncrement.Visible = chkRotatingSequence.Checked;
        }
        private void menuSaveImage_Click(object sender, EventArgs e) {
            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                lineDrawer.GetImage().Save(saveFileDialog.FileName);
            }
        }
        private void numIncrement_ValueChanged(object sender, EventArgs e) {
            lineDrawer.RotatingSequenceAngleIncrement = (int) numIncrement.Value;
        }
        private void numMagnitude_ValueChanged(object sender, EventArgs e) {
            lineDrawer.SpiralDistanceMagnitude = (int) numMagnitude.Value;
        }
    }
}