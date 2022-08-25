using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BGTest {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            cboxRuleCycling.SelectedIndex = 0;
            bg.Reset(new Rectangle(-50, -50, 100, 100), new Rectangle(-10, -10, 20, 20));
            for (int i = 0; i < bg.Area.Width; ++i) {
                bg.Set(bg.MinX + i, bg.MinY + i);
                bg.Set(bg.MinX + i, bg.MaxY - i);
            }
            for (int i = bg.MinY; i <= bg.MaxY; ++i) {
                bg.Set(bg.MinX, i);
                bg.Set(bg.MaxX, i);
            }
            for (int i = bg.MinX; i <= bg.MaxX; ++i) {
                bg.Set(i, bg.MinY);
                bg.Set(i, bg.MaxY);
            }
        }

        private bool ComputeNextIteration(Int64 ruleSet, bool[] curValues) {
            int bitindex = 0;
            for (int i = 0; i < (int)numBreadth.Value; ++i) {
                if (curValues[i]) bitindex += (int)Math.Pow(2, ((int)numBreadth.Value - 1) - i);
            }
            return ((ruleSet >> bitindex) & 1) == 0x01;
        }

        private static bool BitmapsIdentical(Bitmap b1, Bitmap b2, int threshhold) {
            int matchCount = 0;
            int compareCount = 0;
            if (b1 == null && b2 == null) return true;
            if (b1 == null || b2 == null) return false;
            if (b1.Height != b2.Height || b1.Width != b2.Width) return false;
            int totalCount = b1.Height * b1.Width;
            for (int y = 0; y < b1.Height; ++y) {
                for (int x = 0; x < b1.Width; ++x) {
                    ++compareCount;
                    if (b1.GetPixel(x, y) == b2.GetPixel(x, y)) ++matchCount;
                    if (compareCount > totalCount / 5 && (matchCount * 100) / compareCount < threshhold)
                        return false;
                }
            }
            return true;
        }

        private bool inDoAllProcess;

        private void btnDoIt_Click(object sender, EventArgs e) {
            if (chkAll.Checked) {
                inDoAllProcess = true;
                string[] bmpList = Directory.GetFiles(".", "file_*.bmp");
                foreach (string s in bmpList) File.Delete(s);
                // iterate through each of the possible rule sets
                int uniqueCount = 0;
                for (Int64 curRuleSet = 0;
                     curRuleSet < (Int64)Math.Pow(2, Math.Pow(2, (double)numBreadth.Value));
                     ++curRuleSet) {
                    numRuleSet.Value = curRuleSet;
                    numRuleSet.Refresh();
                    numRuleSet.Update();
                    DoRuleSet(curRuleSet);
                    Bitmap curBmp = new Bitmap(bg.Image);
                    bool foundDup = false;
                    if (uniqueCount > 0) {
                        string[] bmpfileList = Directory.GetFiles(".", "file_*.bmp");
                        if (bmpfileList.Select(Image.FromFile).Select(img => new Bitmap(img)).Any(bmp => BitmapsIdentical(bmp, curBmp, (int)numDupThreshold.Value))) {
                            foundDup = true;
                        }
                    }
                    if (!foundDup) {
                        bg.SaveBitmap("file_" + curRuleSet + ".bmp");
                        ++uniqueCount;
                    }
                }
                inDoAllProcess = false;
            }
            else {
                DoRuleSet((int)numRuleSet.Value);
            }
        }
        private readonly Random _rand = new Random();
        private void DoRuleSet(Int64 ruleset) {
            bg.SuspendRefreshes = true;
            bool[] prevValues = new bool[(int)numDepth.Value];
            bool[] newValues = new bool[(int)numDepth.Value];
            bool[] controlValues = new bool[(int)numBreadth.Value];
            bg.Reset(new Rectangle(0, 0, (int)numDepth.Value, (int)numDepth.Value),
                     new Rectangle(0, 0, (int)numDepth.Value, (int)numDepth.Value));
            // perform iteration 0.
            for (int i = 0; i < (int)numDepth.Value; ++i) prevValues[i] = false;
            bool randomized = chkRandomPoints.Checked;
            for (int initpt = 0; initpt < (int)numInitPointCount.Value; ++initpt) {
                int index;
                if (randomized) {
                    do {
                        index = _rand.Next((int)numDepth.Value);
                    } while (prevValues[index]);
                }
                else {
                    index = (initpt + 1) * ((int)numDepth.Value / ((int)numInitPointCount.Value + 1));
                }
                bg.Set(index, 0);
                prevValues[index] = true;
            }
            bool alternator = false;
            // perform iteration 1 through the selected depth.
            for (int iteration = 1; iteration < (int)numDepth.Value; ++iteration) {
                for (int i = 0; i < (int)numDepth.Value; ++i) newValues[i] = false;
                for (int cell = 0; cell < (int)numDepth.Value; ++cell) {
                    for (int breadth = 0; breadth < (int)numBreadth.Value; ++breadth) {
                        int x;
                        if ((int)numBreadth.Value % 2 == 1 || rbtnLeft.Checked) {
                            x = cell - ((int)numBreadth.Value / 2) + breadth;
                        }
                        else if (rbtnRight.Checked) {
                            x = cell - ((int)numBreadth.Value / 2) + breadth + 1;
                        }
                        else {
                            // alternate right/left
                            x = cell - ((int)numBreadth.Value / 2) + breadth + (alternator ? 0 : 1);
                        }
                        if (x >= 0 && x < prevValues.Length)
                            controlValues[breadth] = prevValues[x];
                        else controlValues[breadth] = chkExternalColor.Checked;
                    }
                    if (ComputeNextIteration(ruleset, controlValues)) {
                        bg.Set(cell, iteration);
                        newValues[cell] = true;
                    }
                    switch (cboxRuleCycling.Text) {
                        case "Invert":
                            ruleset = (int)(numBreadth.Value == 3 ? (byte)(~ruleset) : ~ruleset);
                            break;
                        case "Increment":
                            ++ruleset;
                            if (ruleset >= (int)Math.Pow(2, Math.Pow(2, (double)numBreadth.Value)))
                                ruleset = 0;
                            break;
                        case "Decrement":
                            if (ruleset > 0) --ruleset;
                            else ruleset = (int)Math.Pow(2, Math.Pow(2, (double)numBreadth.Value)) - 1;
                            break;
                        case "Cycle":
                            Int64 save1 = ruleset & 1;
                            ruleset >>= 1;
                            if (save1 != 0) ruleset |= 0x80;
                            break;
                    }
                }
                for (int i = 0; i < (int)numDepth.Value; ++i) prevValues[i] = newValues[i];
                alternator = !alternator;
            }
            bg.SuspendRefreshes = false;
            bg.Refresh();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e) {
            numRuleSet.Enabled = !chkAll.Checked;
            lblDupThreshold.Enabled = chkAll.Checked;
            numDupThreshold.Enabled = chkAll.Checked;
        }

        private void Form1_Shown(object sender, EventArgs e) {
            bg.Refresh();
        }

        private void numRuleSet_ValueChanged(object sender, EventArgs e) {
            if (!inDoAllProcess) btnDoIt_Click(sender, e);
        }

        private void numBreadth_ValueChanged(object sender, EventArgs e) {
            numRuleSet.Maximum = (Int64)Math.Pow(2, Math.Pow(2, (double)numBreadth.Value));
            rbtnLeft.Enabled = rbtnAlternate.Enabled = rbtnRight.Enabled = ((int)numBreadth.Value % 2 == 0);
        }

        private void numDepth_ValueChanged(object sender, EventArgs e) {
            btnDoIt_Click(sender, e);
        }

        private void rbtn_CheckedChanged(object sender, EventArgs e) {
            btnDoIt_Click(sender, e);
        }

        private void chkExternalColor_CheckedChanged(object sender, EventArgs e) {
            btnDoIt_Click(sender, e);
        }

        private void numInitPointCount_ValueChanged(object sender, EventArgs e) {
            btnDoIt_Click(sender, e);
        }

        private void chkRandomPoints_CheckedChanged(object sender, EventArgs e) {
            btnDoIt_Click(sender, e);
        }
    }
}