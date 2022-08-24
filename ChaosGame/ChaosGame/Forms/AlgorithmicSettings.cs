using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JSON;

namespace ChaosGame {
    public partial class AlgorithmicSettings : Form {
        public AlgorithmicSettings(JSONObject persistValues) {
            InitializeComponent();
            cboxOnePointMethod.Items.Clear();
            cboxOnePointMethod.Items.AddRange(PickOnePointOptions.ToArray<object>());
            cboxTwoPtMethod.Items.Clear();
            cboxTwoPtMethod.Items.AddRange(PickTwoPointsOptions.ToArray<object>());
            cboxOnePointMove.Items.Clear();
            cboxOnePointMove.Items.AddRange(OnePointMovementOptions.ToArray<object>());
            cboxTwoPointMove.Items.Clear();
            cboxTwoPointMove.Items.AddRange(TwoPointMovementOptions.ToArray<object>());
            if (persistValues != null) PersistentValues = persistValues;
            if (persistValues == null) {
                rbtnOnePoint.Checked = true;
                cboxOnePointMethod.SelectedIndex = 0;
                cboxTwoPtMethod.SelectedIndex = 0;
                cboxOnePointMove.SelectedIndex = 0;
                cboxTwoPointMove.SelectedIndex = 0;
                txtTravelDist.Text = "0.5";
                chkRandomizeBlack.Checked = true;
                chkMixColors.Checked = true;
                chkDrawLines.Checked = false;
                numRadius.Value = 100;
                numPickN.Value = 2;
                numMoveN.Value = 2;
                rbtnFractionOfDist.Checked = true;
                txtConstDist.Text = "100";
            }
            else LoadPersistentValues();
        }
        public ulong MaxIterations { get; set; }
        public JSONObject PersistentValues { get; } = new JSONObject();
        private void cboxOnePointMethod_SelectedIndexChanged(object sender, EventArgs e) {
            PersistentValues.put("OnePointMethod", cboxOnePointMethod.SelectedIndex);
            UpdateUI();
        }
        private void cboxOnePointMove_SelectedIndexChanged(object sender, EventArgs e) {
            PersistentValues.put("OnePointMovement", cboxOnePointMove.SelectedIndex);
            UpdateUI();
        }
        private void cboxTwoPointMove_SelectedIndexChanged(object sender, EventArgs e) {
            PersistentValues.put("TwoPointMovement", cboxTwoPointMove.SelectedIndex);
            UpdateUI();
        }
        private void cboxTwoPtMethod_SelectedIndexChanged(object sender, EventArgs e) {
            PersistentValues.put("TwoPointMethod", cboxTwoPtMethod.SelectedIndex);
            UpdateUI();
        }
        private void chkAlongCircle_CheckedChanged(object sender, EventArgs e) {
            PersistentValues.put("CircularPath", chkAlongCircle.Checked);
            if (chkAlongCircle.Checked) rbtnFractionOfDist.Checked = true;
            rbtnFractionOfMaxDist.Enabled = rbtnFractionOfMinDist.Enabled = rbtnConstantDist.Enabled = !chkAlongCircle.Checked;
        }
        private void chkCombineDist_CheckedChanged(object sender, EventArgs e) {
            PersistentValues.put("ComboMixEnabled", chkCombineDist.Checked);
            UpdateUI();
        }
        private void chkDrawLines_CheckedChanged(object sender, EventArgs e) {
            PersistentValues.put("DrawLines", chkDrawLines.Checked);
        }
        private void chkIncludeCurPnt_CheckedChanged(object sender, EventArgs e) {
            PersistentValues.put("IncludeCurPtInMoveCalc", chkIncludeCurPnt.Checked);
        }
        private void chkMaxIterations_CheckedChanged(object sender, EventArgs e) {
            numMaxIterations.Enabled = chkMaxIterations.Checked;
            PersistentValues.put("MaxIterationsEnabled", chkMaxIterations.Checked);
        }
        private void chkMixColors_CheckedChanged(object sender, EventArgs e) {
            PersistentValues.put("MixColors", chkMixColors.Checked);
        }
        private void chkRandomizeBlack_CheckedChanged(object sender, EventArgs e) {
            PersistentValues.put("RandomizeBlack", chkRandomizeBlack.Checked);
        }
        private void numMaxIterations_ValueChanged(object sender, EventArgs e) {
            MaxIterations = (ulong)numMaxIterations.Value;
            PersistentValues.put("MaxIterations", MaxIterations);
        }
        private void numMoveN_ValueChanged(object sender, EventArgs e) {
            PersistentValues.put("MoveN", (int)numMoveN.Value);
        }
        private void numPickN_ValueChanged(object sender, EventArgs e) {
            PersistentValues.put("PickN", (int)numPickN.Value);
        }
        private void numRadius_ValueChanged(object sender, EventArgs e) {
            PersistentValues.put("PointPickRadius", (int)numRadius.Value);
        }
        private void rbtnDist_CheckedChanged(object sender, EventArgs e) {
            UpdateUI();
            PersistentValues.put("DistanceMeaning",
                                 rbtnFractionOfDist.Checked ? "Fraction of point dist" :
                                 rbtnFractionOfMaxDist.Checked ? "Fraction of max dist" :
                                 rbtnFractionOfMinDist.Checked ? "Fraction of min dist" : "Constant dist");
            //sbarComboMix.Visible = lblPctDist1.Visible = lblPctDist2.Visible = rbtnComboDist.Checked;
        }
        private void rbtnOnePoint_CheckedChanged(object sender, EventArgs e) {
            cboxOnePointMethod.Visible = cboxOnePointMove.Visible = rbtnOnePoint.Checked;
            PersistentValues.put("OnePoint", rbtnOnePoint.Checked);
            UpdateUI();
        }
        private void rbtnTwoPoints_CheckedChanged(object sender, EventArgs e) {
            cboxTwoPtMethod.Visible = cboxTwoPointMove.Visible = rbtnTwoPoints.Checked;
            PersistentValues.put("TwoPoints", rbtnTwoPoints.Checked);
            UpdateUI();
        }
        private void sbarComboMix_ValueChanged(object sender, EventArgs e) {
            UpdateCombineSBar();
            PersistentValues.put("ComboMix", sbarComboMix.Value);
        }
        private void txtConstDist_TextChanged(object sender, EventArgs e) {
            PersistentValues.put("ConstantDist", txtConstDist.Text);
        }
        private void txtTravelDist_TextChanged(object sender, EventArgs e) {
            PersistentValues.put("TravelDist", txtTravelDist.Text);
        }
        private void LoadPersistentValues() {
            rbtnOnePoint.Checked = PersistentValues.optBoolean("OnePoint", rbtnOnePoint.Checked);
            rbtnTwoPoints.Checked = PersistentValues.optBoolean("TwoPoints", rbtnTwoPoints.Checked);
            cboxTwoPtMethod.SelectedIndex = PersistentValues.optInt("TwoPointMethod", cboxTwoPtMethod.SelectedIndex);
            cboxOnePointMethod.SelectedIndex = PersistentValues.optInt("OnePointMethod", cboxOnePointMethod.SelectedIndex);
            cboxOnePointMove.SelectedIndex = PersistentValues.optInt("OnePointMovement", cboxOnePointMove.SelectedIndex);
            cboxTwoPointMove.SelectedIndex = PersistentValues.optInt("TwoPointMovement", cboxTwoPointMove.SelectedIndex);
            txtTravelDist.Text = PersistentValues.optString("TravelDist", txtTravelDist.Text);
            chkRandomizeBlack.Checked = PersistentValues.optBoolean("RandomizeBlack", chkRandomizeBlack.Checked);
            chkMixColors.Checked = PersistentValues.optBoolean("MixColors", chkMixColors.Checked);
            chkDrawLines.Checked = PersistentValues.optBoolean("DrawLines", chkDrawLines.Checked);
            numRadius.Value = PersistentValues.optInt("PointPickRadius", 100);
            numPickN.Value = PersistentValues.optInt("PickN", 2);
            numMoveN.Value = PersistentValues.optInt("MoveN", 2);
            chkIncludeCurPnt.Checked = PersistentValues.optBoolean("IncludeCurPtInMoveCalc");
            switch (PersistentValues.optString("DistanceMeaning")) {
                default:
                case "Fraction of point dist":
                    rbtnFractionOfDist.Checked = true;
                    break;
                case "Fraction of max dist":
                    rbtnFractionOfMaxDist.Checked = true;
                    break;
                case "Fraction of min dist":
                    rbtnFractionOfMinDist.Checked = true;
                    break;
                case "Constant dist":
                    rbtnConstantDist.Checked = true;
                    break;
            }
            sbarComboMix.Value = PersistentValues.optInt("ComboMix");
            chkCombineDist.Checked = PersistentValues.optBoolean("ComboMixEnabled");
            chkMaxIterations.Checked = PersistentValues.optBoolean("MaxIterationsEnabled");
            numMaxIterations.Value = PersistentValues.optInt("MaxIterations");
            txtConstDist.Text = PersistentValues.optString("ConstantDist", txtConstDist.Text);
            chkAlongCircle.Checked = PersistentValues.optBoolean("CircularPath");
        }
        private void UpdateCombineSBar() {
            lblPctDist1.Text = (100 - sbarComboMix.Value) + "% dist to selected point";
            if (rbtnFractionOfMaxDist.Checked) lblPctDist2.Text = sbarComboMix.Value + "% max dist between pts";
            else if (rbtnFractionOfMinDist.Checked) lblPctDist2.Text = sbarComboMix.Value + "% min dist between pts";
            else if (rbtnConstantDist.Checked) lblPctDist2.Text = sbarComboMix.Value + "% constant dist";
            lblPctDist2.Left = sbarComboMix.Right - (lblPctDist2.Width + 10);
        }
        private void UpdateUI() {
            numRadius.Location = rbtnOnePoint.Checked ? new Point(cboxOnePointMethod.Right + 5, cboxOnePointMethod.Top) : new Point(cboxTwoPtMethod.Left - (numPickN.Width + 5), cboxTwoPtMethod.Top);
            numRadius.Visible = rbtnOnePoint.Checked && (cboxOnePointMethod.SelectedItem?.ToString().Contains("radius") ?? false) ||
                                                    rbtnTwoPoints.Checked && (cboxTwoPtMethod.SelectedItem?.ToString().Contains("radius") ?? false) ||
                                                    rbtnOnePoint.Checked && (cboxOnePointMove.SelectedItem?.ToString().Contains("radius") ?? false);
            numPickN.Location = rbtnOnePoint.Checked ? new Point(cboxOnePointMethod.Right + 5, cboxOnePointMethod.Top) : new Point(cboxTwoPtMethod.Left - (numPickN.Width + 5), cboxTwoPtMethod.Top);
            numPickN.Visible = rbtnOnePoint.Checked && (cboxOnePointMethod.SelectedItem?.ToString().Contains("among N") ?? false) ||
                               rbtnTwoPoints.Checked && (cboxTwoPtMethod.SelectedItem?.ToString().Contains("among N") ?? false);
            numMoveN.Location = rbtnOnePoint.Checked ? new Point(cboxOnePointMove.Right + 5, cboxOnePointMove.Top) : new Point(cboxTwoPointMove.Left - (numMoveN.Width + 5), cboxTwoPointMove.Top);
            chkIncludeCurPnt.Location = rbtnOnePoint.Checked ? new Point(numMoveN.Right + 5, numMoveN.Top + 2) : new Point(numMoveN.Left - (chkIncludeCurPnt.Width + 5), numMoveN.Top + 2);
            numMoveN.Visible = chkIncludeCurPnt.Visible = rbtnOnePoint.Checked && (cboxOnePointMove.SelectedItem?.ToString().Contains("last N") ?? false) ||
                                                          rbtnTwoPoints.Checked && (cboxTwoPointMove.SelectedItem?.ToString().Contains("last N") ?? false);
            chkAlongCircle.Location = new Point(numMoveN.Left, numMoveN.Visible ? numMoveN.Bottom + 3 : cboxOnePointMove.Top + 2);
            chkAlongCircle.Visible = rbtnOnePoint.Checked;
            if (rbtnTwoPoints.Checked && (cboxTwoPointMove.SelectedItem?.ToString().Contains("3-point arc") ?? false)) {
                rbtnFractionOfDist.Checked = true;
                rbtnFractionOfMaxDist.Enabled = rbtnFractionOfMinDist.Enabled = rbtnConstantDist.Enabled = false;
            }
            else {
                rbtnFractionOfMaxDist.Enabled = rbtnFractionOfMinDist.Enabled = rbtnConstantDist.Enabled = true;
            }
            sbarComboMix.Visible = lblPctDist1.Visible = lblPctDist2.Visible = chkCombineDist.Checked && !rbtnFractionOfDist.Checked;
            chkCombineDist.Enabled = !rbtnFractionOfDist.Checked;
            if (rbtnFractionOfDist.Checked) chkCombineDist.Checked = false;
            txtConstDist.Visible = rbtnConstantDist.Checked;
            UpdateCombineSBar();
        }
        public static readonly string[] OnePointMovementOptions = {
                                                                      "toward point",
                                                                      "toward avg last N pts",
                                                                  };
        public static readonly string[] PickOnePointOptions = {
                                                                  "at random",
                                                                  "among N nearest points",
                                                                  "among N farthest points",
                                                                  "within limited radius"
                                                              };
        public static readonly string[] PickTwoPointsOptions = {
                                                                   "at random",
                                                                   "among N nearest points",
                                                                   "among N farthest points",
                                                                   "within limited radius"
                                                               };
        public static readonly string[] TwoPointMovementOptions = {
                                                                      "toward center point",
                                                                      "along 3-point arc"
                                                                  };
    }
}