using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CyclicalAutomata.Properties;

namespace CyclicalAutomata {
   public partial class Control : Form {
      public Control() {
         InitializeComponent();
         _size = Size;
         cboxInitialCond.Items.AddRange(new object[] {"Random", "Ordered", "From file"});
         cboxInitialCond.SelectedIndex = 0;
         cboxRule.Items.AddRange(Enum.GetNames(typeof (CyclicalAutomata.Rules)).Cast<object>().ToArray());
         cboxRule.SelectedIndex = Settings.Default.Rule;
         cboxAgeEffect.SelectedIndex = 0;
      }
      public Control(MainForm parent) : this() {
         _parent = parent;
         txtDimensions.Text = _parent.Automata.Size.Width + ", " + _parent.Automata.Size.Height;
         txtWindowSize.Text = _parent.Size.Width + ", " + _parent.Size.Height;
         numStates.Value = _parent.Automata.StateCount;
         numThreshold.Value = _parent.Automata.ThresholdLow;
         numThresholdHigh.Value = _parent.Automata.ThresholdHigh;
         numUIDelay.Value = _parent.timer.Interval;
      }
      private void btnExit_Click(object sender, EventArgs e) {
         _parent.Close();
      }
      private void btnReset_Click(object sender, EventArgs e) {
         _parent.Automata.Reset();
      }
      private void btnSetDimensions_Click(object sender, EventArgs e) {
         string[] strs = txtDimensions.Text.Split(',');
         _parent.Automata.Size = new Size(int.Parse(strs[0]), int.Parse(strs[1]));
         Settings.Default.AutomataSize = _parent.Automata.Size;
         Settings.Default.Save();
      }
      private void btnSetWindowSize_Click(object sender, EventArgs e) {
         string[] strs = txtWindowSize.Text.Split(',');
         _parent.Size = new Size(int.Parse(strs[0]), int.Parse(strs[1]));
      }
      private void btnStartStop_Click(object sender, EventArgs e) {
         if (_parent.Running) {
            _parent.Stop();
            btnStartStop.Text = "Start";
         }
         else {
            _parent.Start();
            btnStartStop.Text = "Stop";
         }
         btnReset.Enabled = cboxInitialCond.Enabled = !_parent.Running;
      }
      private void cboxAgeEffect_SelectedIndexChanged(object sender, EventArgs e) {
         if (_parent != null) _parent.Automata.AgeEffect = (CyclicalAutomata.AgeEffects) cboxAgeEffect.SelectedIndex;
      }
      private void cboxInitialCond_SelectedIndexChanged(object sender, EventArgs e) {
         if (_parent == null) return;
         switch (cboxInitialCond.SelectedItem.ToString()) {
            case "Random":
               _parent.Automata.Initial = CyclicalAutomata.InitialCond.Random;
               break;
            case "Ordered":
               _parent.Automata.Initial = CyclicalAutomata.InitialCond.Ordered;
               break;
            case "From file":
               if (openFileDialog.ShowDialog() == DialogResult.OK) {
                  _parent.Automata.Initial = CyclicalAutomata.InitialCond.FromFile;
                  _parent.Automata.InitializeFromFile(openFileDialog.FileName);
               }
               else cboxInitialCond.SelectedIndex = (int) _parent.Automata.Initial;
               break;
         }
      }
      private void cboxRule_SelectedIndexChanged(object sender, EventArgs e) {
         var rule = (CyclicalAutomata.Rules) Enum.Parse(typeof (CyclicalAutomata.Rules), cboxRule.SelectedItem.ToString());
         if (_parent != null) _parent.Automata.Rule = rule;
         Settings.Default.Rule = (int) rule;
         Settings.Default.Save();
         numThreshold.Visible = false;
         numThresholdHigh.Visible = false;
         switch (rule) {
            case CyclicalAutomata.Rules.NextOnly:
               lblDescription.Text = "Rule:\n\n" +
                                     "For each cell: if at least " +
                                     (_parent == null || _parent.Automata.ThresholdLow == 1 ? "one" : _parent.Automata.ThresholdLow.ToString()) +
                                     " of its eight neighbors is at the next state, the cell goes to that state.\n\n" +
                                     "Otherwise do not change state.";
               numThreshold.Visible = true;
               numThresholdHigh.Visible = true;
               break;
            case CyclicalAutomata.Rules.PrevOnly:
               lblDescription.Text = "Rule:\n\n" +
                                     "For each cell: if at least " +
                                     (_parent == null || _parent.Automata.ThresholdLow == 1 ? "one" : _parent.Automata.ThresholdLow.ToString()) +
                                     " of its eight neighbors is at the previous state, the cell goes to that state.\n\n" +
                                     "Otherwise do not change state.";
               numThreshold.Visible = true;
               numThresholdHigh.Visible = true;
               break;
            case CyclicalAutomata.Rules.NextIfDouble:
               lblDescription.Text = "Rules:\n\n" +
                                     "For each cell: \n\n" +
                                     "1. If at least twice as many of its neighbors are at the next state than are at the previous state, go to the next state.\n\n" +
                                     "2. Otherwise do not change state.";
               break;
            case CyclicalAutomata.Rules.NextIfTriple:
               lblDescription.Text = "Rules:\n\n" +
                                     "For each cell: \n\n" +
                                     "1. If at least three times as many of its neighbors are at the next state than are at the previous state, go to the next state.\n\n" +
                                     "2. Otherwise do not change state.";
               break;
            case CyclicalAutomata.Rules.NextIfQuad:
               lblDescription.Text = "Rules:\n\n" +
                                     "For each cell: \n\n" +
                                     "1. If at least four times as many of its neighbors are at the next state than are at the previous state, go to the next state.\n\n" +
                                     "2. Otherwise do not change state.";
               break;
            case CyclicalAutomata.Rules.NextIfQuint:
               lblDescription.Text = "Rules:\n\n" +
                                     "For each cell: \n\n" +
                                     "1. If at least five times as many of its neighbors are at the next state than are at the previous state, go to the next state.\n\n" +
                                     "2. Otherwise do not change state.";
               break;
            case CyclicalAutomata.Rules.NextOrPrev:
               lblDescription.Text = "Rules:\n\n" +
                                     "For each cell: \n\n" +
                                     "1. If at least one of its eight neighbors is at the next state, but none are at the previous state, go to the next state.\n\n" +
                                     "2. Otherwise, if at least one of its neighbors is at the previous state, but none are at the next state, go to the previous state.\n\n" +
                                     "3. Otherwise do not change state.";
               numThreshold.Visible = true;
               numThresholdHigh.Visible = true;
               break;
            case CyclicalAutomata.Rules.FavorNext:
               lblDescription.Text = "Rules:\n\n" +
                                     "For each cell: \n\n" +
                                     "1. If at least one of its eight neighbors is at the next state, go to the next state.\n\n" +
                                     "2. Otherwise, if at least one of its neighbors is at the previous state, go to the previous state.\n\n" +
                                     "3. Otherwise do not change state.";
               break;
            case CyclicalAutomata.Rules.NextIfMore:
               lblDescription.Text = "Rules:\n\n" +
                                     "For each cell: \n\n" +
                                     "1. If more of its neighbors are at the next state than are at the previous state, go to the next state.\n\n" +
                                     "2. Otherwise do not change state.";
               break;
            case CyclicalAutomata.Rules.NextIfZero:
               lblDescription.Text = "Rules:\n\n" +
                                     "For each cell: \n\n" +
                                     "1. If none of its neighbors are at the next state, go to the next state.\n\n" +
                                     "2. Otherwise do not change state.";
               break;
            case CyclicalAutomata.Rules.NextProgressive:
               lblDescription.Text = "Rules:\n\n" +
                                     "For each cell: \n\n" +
                                     "1. If at least TWO of its neighbors are at its state + 1, go to that state.\n\n" +
                                     "2. Otherwise, if at least THREE of its neighbors are at its state + 2, go to that state (skip 1 state).\n\n" +
                                     "3. Otherwise, if at least FOUR of its neighbors are at its state + 3, go to that state.\n\n" +
                                     "4. Continue this pattern through all states (except the cell's current state).\n\n" +
                                     "5. If no states are found to go to in the previous steps, do not change state.";
               break;
            case CyclicalAutomata.Rules.NextFavorDiagonal:
               lblDescription.Text = "Rules:\n\n" +
                                     "For each cell: \n\n" +
                                     "1. Go to the next state if at least one of its DIAGONAL neighbors are at that state.\n\n" +
                                     "2. Otherwise do not change state.";
               break;
            case CyclicalAutomata.Rules.NextFavorOrthogonal:
               lblDescription.Text = "Rules:\n\n" +
                                     "For each cell: \n\n" +
                                     "1. Go to the next state if at least one of its ORTHOGONAL neighbors are at that state.\n\n" +
                                     "2. Otherwise do not change state.";
               break;
            case CyclicalAutomata.Rules.NextRotatingFavor:
               lblDescription.Text = "Rules\n\n" +
                                     "For each cell: \n\n" +
                                     "1. Go to the next state if:\n" +
                                     "- its neighbor in the favored direction is at that state, or\n" +
                                     "- at least FIVE of it's other neighbors are at that state.\n\n" +
                                     "2. Otherwise do not change state.\n\n" +
                                     "The \"favored\" direction rotates continuously in a clockwise direction, a step at a time, with each iteration.";
               break;
            default:
               lblDescription.Text = "No description available.";
               break;
         }
      }
      private void chkRandomizeColors_CheckedChanged(object sender, EventArgs e) {
         _parent.Automata.StateCount = (int) numStates.Value;
         if (chkRandomizeColors.Checked) _parent.Automata.RandomizeColors();
         Settings.Default.RandomizeColors = chkRandomizeColors.Checked;
         Settings.Default.Save();
      }
      private void Control_Activated(object sender, EventArgs e) {
         //if (_parent != null && !_parent.V) _parent.BringToFront();
      }
      private void Control_Shown(object sender, EventArgs e) {
         Location = new Point(_parent.Right, _parent.Top);
         Size = _size;
      }
      private void lblIterations_TextChanged(object sender, EventArgs e) {
         lblIterations.Left = ClientRectangle.Width - lblIterations.Width;
      }
      private void numAgeSpeed_ValueChanged(object sender, EventArgs e) {
         if (_parent != null) _parent.Automata.AgeSpeed = (int) numAgeSpeed.Value;
      }
      private void numDelay_ValueChanged(object sender, EventArgs e) {
         if (_parent != null) _parent.Delay = (int) numDelay.Value;
      }
      private void numStates_ValueChanged(object sender, EventArgs e) {
         if (_parent != null) {
            _parent.Automata.StateCount = (int) numStates.Value;
            if (chkRandomizeColors.Checked) _parent.Automata.RandomizeColors();
            Settings.Default.StateCount = (int) numStates.Value;
            Settings.Default.Save();
         }
      }
      private void numThresholdHigh_ValueChanged(object sender, EventArgs e) {
         if (_parent != null) _parent.Automata.ThresholdHigh = (int) numThresholdHigh.Value;
      }
      private void numThreshold_ValueChanged(object sender, EventArgs e) {
         if (_parent != null) {
            int oldval = _parent.Automata.ThresholdLow;
            _parent.Automata.ThresholdLow = (int) numThreshold.Value;
            if (_parent.Automata.Rule == CyclicalAutomata.Rules.NextOnly || _parent.Automata.Rule == CyclicalAutomata.Rules.PrevOnly) {
               lblDescription.Text = lblDescription.Text.Replace("at least " + (oldval == 1 ? "one" : oldval.ToString()),
                                                                 "at least " + (_parent.Automata.ThresholdLow == 1 ? "one" : _parent.Automata.ThresholdLow.ToString()));
            }
         }
      }
      private void numUIDelay_ValueChanged(object sender, EventArgs e) {
         if (_parent != null) _parent.timer.Interval = (int) numUIDelay.Value;
      }
      private readonly MainForm _parent;
      private readonly Size _size;
   }
}