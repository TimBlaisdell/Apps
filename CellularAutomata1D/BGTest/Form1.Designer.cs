namespace BGTest {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing ) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ( ) {
            this.btnDoIt = new System.Windows.Forms.Button();
            this.numRuleSet = new System.Windows.Forms.NumericUpDown();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.numDepth = new System.Windows.Forms.NumericUpDown();
            this.lblDepth = new System.Windows.Forms.Label();
            this.numBreadth = new System.Windows.Forms.NumericUpDown();
            this.lblBreadth = new System.Windows.Forms.Label();
            this.numDupThreshold = new System.Windows.Forms.NumericUpDown();
            this.lblDupThreshold = new System.Windows.Forms.Label();
            this.cboxRuleCycling = new System.Windows.Forms.ComboBox();
            this.lblRuleCycling = new System.Windows.Forms.Label();
            this.rbtnLeft = new System.Windows.Forms.RadioButton();
            this.rbtnAlternate = new System.Windows.Forms.RadioButton();
            this.rbtnRight = new System.Windows.Forms.RadioButton();
            this.chkExternalColor = new System.Windows.Forms.CheckBox();
            this.numInitPointCount = new System.Windows.Forms.NumericUpDown();
            this.lblInitialPointCount = new System.Windows.Forms.Label();
            this.bg = new BG.BlockGraphics();
            this.chkRandomPoints = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numRuleSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBreadth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDupThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInitPointCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bg)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDoIt
            // 
            this.btnDoIt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDoIt.Location = new System.Drawing.Point(12, 592);
            this.btnDoIt.Name = "btnDoIt";
            this.btnDoIt.Size = new System.Drawing.Size(75, 23);
            this.btnDoIt.TabIndex = 1;
            this.btnDoIt.Text = "Do It";
            this.btnDoIt.UseVisualStyleBackColor = true;
            this.btnDoIt.Click += new System.EventHandler(this.btnDoIt_Click);
            // 
            // numRuleSet
            // 
            this.numRuleSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numRuleSet.Location = new System.Drawing.Point(93, 595);
            this.numRuleSet.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numRuleSet.Name = "numRuleSet";
            this.numRuleSet.Size = new System.Drawing.Size(67, 20);
            this.numRuleSet.TabIndex = 2;
            this.numRuleSet.ValueChanged += new System.EventHandler(this.numRuleSet_ValueChanged);
            // 
            // chkAll
            // 
            this.chkAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(166, 596);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(37, 17);
            this.chkAll.TabIndex = 3;
            this.chkAll.Text = "All";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // numDepth
            // 
            this.numDepth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numDepth.Location = new System.Drawing.Point(563, 595);
            this.numDepth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numDepth.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numDepth.Name = "numDepth";
            this.numDepth.Size = new System.Drawing.Size(65, 20);
            this.numDepth.TabIndex = 4;
            this.numDepth.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.numDepth.ValueChanged += new System.EventHandler(this.numDepth_ValueChanged);
            // 
            // lblDepth
            // 
            this.lblDepth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDepth.AutoSize = true;
            this.lblDepth.Location = new System.Drawing.Point(521, 597);
            this.lblDepth.Name = "lblDepth";
            this.lblDepth.Size = new System.Drawing.Size(36, 13);
            this.lblDepth.TabIndex = 5;
            this.lblDepth.Text = "Depth";
            // 
            // numBreadth
            // 
            this.numBreadth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numBreadth.Location = new System.Drawing.Point(450, 595);
            this.numBreadth.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numBreadth.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numBreadth.Name = "numBreadth";
            this.numBreadth.Size = new System.Drawing.Size(65, 20);
            this.numBreadth.TabIndex = 4;
            this.numBreadth.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numBreadth.ValueChanged += new System.EventHandler(this.numBreadth_ValueChanged);
            // 
            // lblBreadth
            // 
            this.lblBreadth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBreadth.AutoSize = true;
            this.lblBreadth.Location = new System.Drawing.Point(400, 597);
            this.lblBreadth.Name = "lblBreadth";
            this.lblBreadth.Size = new System.Drawing.Size(44, 13);
            this.lblBreadth.TabIndex = 5;
            this.lblBreadth.Text = "Breadth";
            // 
            // numDupThreshold
            // 
            this.numDupThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numDupThreshold.Enabled = false;
            this.numDupThreshold.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numDupThreshold.Location = new System.Drawing.Point(93, 621);
            this.numDupThreshold.Name = "numDupThreshold";
            this.numDupThreshold.Size = new System.Drawing.Size(67, 20);
            this.numDupThreshold.TabIndex = 4;
            this.numDupThreshold.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblDupThreshold
            // 
            this.lblDupThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDupThreshold.AutoSize = true;
            this.lblDupThreshold.Enabled = false;
            this.lblDupThreshold.Location = new System.Drawing.Point(10, 623);
            this.lblDupThreshold.Name = "lblDupThreshold";
            this.lblDupThreshold.Size = new System.Drawing.Size(77, 13);
            this.lblDupThreshold.TabIndex = 5;
            this.lblDupThreshold.Text = "Dup Threshold";
            // 
            // cboxRuleCycling
            // 
            this.cboxRuleCycling.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboxRuleCycling.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxRuleCycling.FormattingEnabled = true;
            this.cboxRuleCycling.Items.AddRange(new object[] {
            "None",
            "Invert",
            "Increment",
            "Decrement",
            "Cycle"});
            this.cboxRuleCycling.Location = new System.Drawing.Point(93, 647);
            this.cboxRuleCycling.Name = "cboxRuleCycling";
            this.cboxRuleCycling.Size = new System.Drawing.Size(121, 21);
            this.cboxRuleCycling.TabIndex = 6;
            // 
            // lblRuleCycling
            // 
            this.lblRuleCycling.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRuleCycling.AutoSize = true;
            this.lblRuleCycling.Location = new System.Drawing.Point(10, 650);
            this.lblRuleCycling.Name = "lblRuleCycling";
            this.lblRuleCycling.Size = new System.Drawing.Size(66, 13);
            this.lblRuleCycling.TabIndex = 5;
            this.lblRuleCycling.Text = "Rule Cycling";
            // 
            // rbtnLeft
            // 
            this.rbtnLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rbtnLeft.AutoSize = true;
            this.rbtnLeft.Enabled = false;
            this.rbtnLeft.Location = new System.Drawing.Point(401, 621);
            this.rbtnLeft.Name = "rbtnLeft";
            this.rbtnLeft.Size = new System.Drawing.Size(43, 17);
            this.rbtnLeft.TabIndex = 7;
            this.rbtnLeft.Text = "Left";
            this.rbtnLeft.UseVisualStyleBackColor = true;
            this.rbtnLeft.CheckedChanged += new System.EventHandler(this.rbtn_CheckedChanged);
            // 
            // rbtnAlternate
            // 
            this.rbtnAlternate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rbtnAlternate.AutoSize = true;
            this.rbtnAlternate.Checked = true;
            this.rbtnAlternate.Enabled = false;
            this.rbtnAlternate.Location = new System.Drawing.Point(450, 621);
            this.rbtnAlternate.Name = "rbtnAlternate";
            this.rbtnAlternate.Size = new System.Drawing.Size(67, 17);
            this.rbtnAlternate.TabIndex = 7;
            this.rbtnAlternate.TabStop = true;
            this.rbtnAlternate.Text = "Alternate";
            this.rbtnAlternate.UseVisualStyleBackColor = true;
            this.rbtnAlternate.CheckedChanged += new System.EventHandler(this.rbtn_CheckedChanged);
            // 
            // rbtnRight
            // 
            this.rbtnRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rbtnRight.AutoSize = true;
            this.rbtnRight.Enabled = false;
            this.rbtnRight.Location = new System.Drawing.Point(523, 621);
            this.rbtnRight.Name = "rbtnRight";
            this.rbtnRight.Size = new System.Drawing.Size(50, 17);
            this.rbtnRight.TabIndex = 7;
            this.rbtnRight.Text = "Right";
            this.rbtnRight.UseVisualStyleBackColor = true;
            this.rbtnRight.CheckedChanged += new System.EventHandler(this.rbtn_CheckedChanged);
            // 
            // chkExternalColor
            // 
            this.chkExternalColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkExternalColor.AutoSize = true;
            this.chkExternalColor.Location = new System.Drawing.Point(471, 644);
            this.chkExternalColor.Name = "chkExternalColor";
            this.chkExternalColor.Size = new System.Drawing.Size(155, 17);
            this.chkExternalColor.TabIndex = 8;
            this.chkExternalColor.Text = "Off-Image Blocks are Black";
            this.chkExternalColor.UseVisualStyleBackColor = true;
            this.chkExternalColor.CheckedChanged += new System.EventHandler(this.chkExternalColor_CheckedChanged);
            // 
            // numInitPointCount
            // 
            this.numInitPointCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numInitPointCount.Location = new System.Drawing.Point(226, 621);
            this.numInitPointCount.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.numInitPointCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numInitPointCount.Name = "numInitPointCount";
            this.numInitPointCount.Size = new System.Drawing.Size(67, 20);
            this.numInitPointCount.TabIndex = 4;
            this.numInitPointCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numInitPointCount.ValueChanged += new System.EventHandler(this.numInitPointCount_ValueChanged);
            // 
            // lblInitialPointCount
            // 
            this.lblInitialPointCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInitialPointCount.AutoSize = true;
            this.lblInitialPointCount.Location = new System.Drawing.Point(171, 623);
            this.lblInitialPointCount.Name = "lblInitialPointCount";
            this.lblInitialPointCount.Size = new System.Drawing.Size(49, 13);
            this.lblInitialPointCount.TabIndex = 5;
            this.lblInitialPointCount.Text = "Initial Pts";
            // 
            // bg
            // 
            this.bg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bg.Location = new System.Drawing.Point(12, 12);
            this.bg.Mode = BG.BlockGraphics.ModeEnum.Zoom;
            this.bg.Name = "bg";
            this.bg.Size = new System.Drawing.Size(616, 574);
            this.bg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bg.SuspendRefreshes = false;
            this.bg.TabIndex = 0;
            this.bg.TabStop = false;
            this.bg.ViewArea = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // chkRandomPoints
            // 
            this.chkRandomPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkRandomPoints.AutoSize = true;
            this.chkRandomPoints.Location = new System.Drawing.Point(299, 624);
            this.chkRandomPoints.Name = "chkRandomPoints";
            this.chkRandomPoints.Size = new System.Drawing.Size(85, 17);
            this.chkRandomPoints.TabIndex = 8;
            this.chkRandomPoints.Text = "Randomized";
            this.chkRandomPoints.UseVisualStyleBackColor = true;
            this.chkRandomPoints.CheckedChanged += new System.EventHandler(this.chkRandomPoints_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 683);
            this.Controls.Add(this.chkRandomPoints);
            this.Controls.Add(this.chkExternalColor);
            this.Controls.Add(this.rbtnRight);
            this.Controls.Add(this.rbtnAlternate);
            this.Controls.Add(this.rbtnLeft);
            this.Controls.Add(this.cboxRuleCycling);
            this.Controls.Add(this.lblRuleCycling);
            this.Controls.Add(this.lblInitialPointCount);
            this.Controls.Add(this.lblDupThreshold);
            this.Controls.Add(this.lblBreadth);
            this.Controls.Add(this.numInitPointCount);
            this.Controls.Add(this.numDupThreshold);
            this.Controls.Add(this.lblDepth);
            this.Controls.Add(this.numBreadth);
            this.Controls.Add(this.numDepth);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.numRuleSet);
            this.Controls.Add(this.btnDoIt);
            this.Controls.Add(this.bg);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numRuleSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBreadth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDupThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInitPointCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BG.BlockGraphics bg;
        private System.Windows.Forms.Button btnDoIt;
        private System.Windows.Forms.NumericUpDown numRuleSet;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.NumericUpDown numDepth;
        private System.Windows.Forms.Label lblDepth;
        private System.Windows.Forms.NumericUpDown numBreadth;
        private System.Windows.Forms.Label lblBreadth;
        private System.Windows.Forms.NumericUpDown numDupThreshold;
        private System.Windows.Forms.Label lblDupThreshold;
        private System.Windows.Forms.ComboBox cboxRuleCycling;
        private System.Windows.Forms.Label lblRuleCycling;
        private System.Windows.Forms.RadioButton rbtnLeft;
        private System.Windows.Forms.RadioButton rbtnAlternate;
        private System.Windows.Forms.RadioButton rbtnRight;
        private System.Windows.Forms.CheckBox chkExternalColor;
        private System.Windows.Forms.NumericUpDown numInitPointCount;
        private System.Windows.Forms.Label lblInitialPointCount;
        private System.Windows.Forms.CheckBox chkRandomPoints;
    }
}

