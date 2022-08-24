namespace CyclicalAutomata {
   partial class Control {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing) {
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
      private void InitializeComponent() {
         this.btnStartStop = new System.Windows.Forms.Button();
         this.btnReset = new System.Windows.Forms.Button();
         this.cboxInitialCond = new System.Windows.Forms.ComboBox();
         this.btnExit = new System.Windows.Forms.Button();
         this.txtDimensions = new System.Windows.Forms.TextBox();
         this.lblDimensions = new System.Windows.Forms.Label();
         this.btnSetDimensions = new System.Windows.Forms.Button();
         this.btnSetWindowSize = new System.Windows.Forms.Button();
         this.txtWindowSize = new System.Windows.Forms.TextBox();
         this.lblWindowSize = new System.Windows.Forms.Label();
         this.cboxRule = new System.Windows.Forms.ComboBox();
         this.lblDescription = new System.Windows.Forms.Label();
         this.numDelay = new System.Windows.Forms.NumericUpDown();
         this.label1 = new System.Windows.Forms.Label();
         this.numStates = new System.Windows.Forms.NumericUpDown();
         this.label2 = new System.Windows.Forms.Label();
         this.chkRandomizeColors = new System.Windows.Forms.CheckBox();
         this.cboxAgeEffect = new System.Windows.Forms.ComboBox();
         this.label3 = new System.Windows.Forms.Label();
         this.numAgeSpeed = new System.Windows.Forms.NumericUpDown();
         this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
         this.lblIterations = new System.Windows.Forms.Label();
         this.numThreshold = new System.Windows.Forms.NumericUpDown();
         this.numUIDelay = new System.Windows.Forms.NumericUpDown();
         this.numThresholdHigh = new System.Windows.Forms.NumericUpDown();
         ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.numStates)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.numAgeSpeed)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.numThreshold)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.numUIDelay)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.numThresholdHigh)).BeginInit();
         this.SuspendLayout();
         // 
         // btnStartStop
         // 
         this.btnStartStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnStartStop.Location = new System.Drawing.Point(197, 387);
         this.btnStartStop.Name = "btnStartStop";
         this.btnStartStop.Size = new System.Drawing.Size(75, 23);
         this.btnStartStop.TabIndex = 0;
         this.btnStartStop.Text = "Start";
         this.btnStartStop.UseVisualStyleBackColor = true;
         this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
         // 
         // btnReset
         // 
         this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.btnReset.Location = new System.Drawing.Point(0, 387);
         this.btnReset.Name = "btnReset";
         this.btnReset.Size = new System.Drawing.Size(75, 23);
         this.btnReset.TabIndex = 0;
         this.btnReset.Text = "Reset";
         this.btnReset.UseVisualStyleBackColor = true;
         this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
         // 
         // cboxInitialCond
         // 
         this.cboxInitialCond.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.cboxInitialCond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cboxInitialCond.FormattingEnabled = true;
         this.cboxInitialCond.Location = new System.Drawing.Point(81, 387);
         this.cboxInitialCond.Name = "cboxInitialCond";
         this.cboxInitialCond.Size = new System.Drawing.Size(84, 21);
         this.cboxInitialCond.TabIndex = 1;
         this.cboxInitialCond.SelectedIndexChanged += new System.EventHandler(this.cboxInitialCond_SelectedIndexChanged);
         // 
         // btnExit
         // 
         this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnExit.Location = new System.Drawing.Point(278, 387);
         this.btnExit.Name = "btnExit";
         this.btnExit.Size = new System.Drawing.Size(75, 23);
         this.btnExit.TabIndex = 0;
         this.btnExit.Text = "Exit";
         this.btnExit.UseVisualStyleBackColor = true;
         this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
         // 
         // txtDimensions
         // 
         this.txtDimensions.Location = new System.Drawing.Point(0, 15);
         this.txtDimensions.Name = "txtDimensions";
         this.txtDimensions.Size = new System.Drawing.Size(63, 20);
         this.txtDimensions.TabIndex = 2;
         // 
         // lblDimensions
         // 
         this.lblDimensions.AutoSize = true;
         this.lblDimensions.Location = new System.Drawing.Point(-3, -1);
         this.lblDimensions.Name = "lblDimensions";
         this.lblDimensions.Size = new System.Drawing.Size(83, 13);
         this.lblDimensions.TabIndex = 3;
         this.lblDimensions.Text = "Dimensions (x,y)";
         // 
         // btnSetDimensions
         // 
         this.btnSetDimensions.Location = new System.Drawing.Point(69, 14);
         this.btnSetDimensions.Name = "btnSetDimensions";
         this.btnSetDimensions.Size = new System.Drawing.Size(34, 21);
         this.btnSetDimensions.TabIndex = 0;
         this.btnSetDimensions.Text = "Set";
         this.btnSetDimensions.UseVisualStyleBackColor = true;
         this.btnSetDimensions.Click += new System.EventHandler(this.btnSetDimensions_Click);
         // 
         // btnSetWindowSize
         // 
         this.btnSetWindowSize.Location = new System.Drawing.Point(178, 15);
         this.btnSetWindowSize.Name = "btnSetWindowSize";
         this.btnSetWindowSize.Size = new System.Drawing.Size(34, 20);
         this.btnSetWindowSize.TabIndex = 0;
         this.btnSetWindowSize.Text = "Set";
         this.btnSetWindowSize.UseVisualStyleBackColor = true;
         this.btnSetWindowSize.Click += new System.EventHandler(this.btnSetWindowSize_Click);
         // 
         // txtWindowSize
         // 
         this.txtWindowSize.Location = new System.Drawing.Point(109, 15);
         this.txtWindowSize.Name = "txtWindowSize";
         this.txtWindowSize.Size = new System.Drawing.Size(63, 20);
         this.txtWindowSize.TabIndex = 2;
         // 
         // lblWindowSize
         // 
         this.lblWindowSize.AutoSize = true;
         this.lblWindowSize.Location = new System.Drawing.Point(109, -1);
         this.lblWindowSize.Name = "lblWindowSize";
         this.lblWindowSize.Size = new System.Drawing.Size(89, 13);
         this.lblWindowSize.TabIndex = 3;
         this.lblWindowSize.Text = "Window size (x,y)";
         // 
         // cboxRule
         // 
         this.cboxRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cboxRule.FormattingEnabled = true;
         this.cboxRule.Location = new System.Drawing.Point(0, 80);
         this.cboxRule.Name = "cboxRule";
         this.cboxRule.Size = new System.Drawing.Size(145, 21);
         this.cboxRule.TabIndex = 1;
         this.cboxRule.SelectedIndexChanged += new System.EventHandler(this.cboxRule_SelectedIndexChanged);
         // 
         // lblDescription
         // 
         this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.lblDescription.Location = new System.Drawing.Point(3, 104);
         this.lblDescription.Name = "lblDescription";
         this.lblDescription.Size = new System.Drawing.Size(343, 280);
         this.lblDescription.TabIndex = 4;
         this.lblDescription.Text = "Description goes here.";
         // 
         // numDelay
         // 
         this.numDelay.Location = new System.Drawing.Point(218, 15);
         this.numDelay.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
         this.numDelay.Name = "numDelay";
         this.numDelay.Size = new System.Drawing.Size(46, 20);
         this.numDelay.TabIndex = 5;
         this.numDelay.ValueChanged += new System.EventHandler(this.numDelay_ValueChanged);
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(215, -1);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(82, 13);
         this.label1.TabIndex = 3;
         this.label1.Text = "Delays (CA, UI):";
         // 
         // numStates
         // 
         this.numStates.Location = new System.Drawing.Point(0, 54);
         this.numStates.Maximum = new decimal(new int[] {
            1535,
            0,
            0,
            0});
         this.numStates.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
         this.numStates.Name = "numStates";
         this.numStates.Size = new System.Drawing.Size(64, 20);
         this.numStates.TabIndex = 5;
         this.numStates.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
         this.numStates.ValueChanged += new System.EventHandler(this.numStates_ValueChanged);
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(-3, 38);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(67, 13);
         this.label2.TabIndex = 3;
         this.label2.Text = "No. of states";
         // 
         // chkRandomizeColors
         // 
         this.chkRandomizeColors.AutoSize = true;
         this.chkRandomizeColors.Location = new System.Drawing.Point(70, 55);
         this.chkRandomizeColors.Name = "chkRandomizeColors";
         this.chkRandomizeColors.Size = new System.Drawing.Size(110, 17);
         this.chkRandomizeColors.TabIndex = 6;
         this.chkRandomizeColors.Text = "Randomize colors";
         this.chkRandomizeColors.UseVisualStyleBackColor = true;
         this.chkRandomizeColors.CheckedChanged += new System.EventHandler(this.chkRandomizeColors_CheckedChanged);
         // 
         // cboxAgeEffect
         // 
         this.cboxAgeEffect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cboxAgeEffect.FormattingEnabled = true;
         this.cboxAgeEffect.Items.AddRange(new object[] {
            "None",
            "Go to gray",
            "Go to black",
            "Go to white"});
         this.cboxAgeEffect.Location = new System.Drawing.Point(186, 53);
         this.cboxAgeEffect.Name = "cboxAgeEffect";
         this.cboxAgeEffect.Size = new System.Drawing.Size(103, 21);
         this.cboxAgeEffect.TabIndex = 1;
         this.cboxAgeEffect.SelectedIndexChanged += new System.EventHandler(this.cboxAgeEffect_SelectedIndexChanged);
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(186, 38);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(64, 13);
         this.label3.TabIndex = 3;
         this.label3.Text = "Aging effect";
         // 
         // numAgeSpeed
         // 
         this.numAgeSpeed.Location = new System.Drawing.Point(295, 54);
         this.numAgeSpeed.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
         this.numAgeSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.numAgeSpeed.Name = "numAgeSpeed";
         this.numAgeSpeed.Size = new System.Drawing.Size(57, 20);
         this.numAgeSpeed.TabIndex = 5;
         this.numAgeSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.numAgeSpeed.ValueChanged += new System.EventHandler(this.numAgeSpeed_ValueChanged);
         // 
         // lblIterations
         // 
         this.lblIterations.AutoSize = true;
         this.lblIterations.Location = new System.Drawing.Point(339, -1);
         this.lblIterations.Name = "lblIterations";
         this.lblIterations.Size = new System.Drawing.Size(13, 13);
         this.lblIterations.TabIndex = 7;
         this.lblIterations.Text = "0";
         this.lblIterations.TextChanged += new System.EventHandler(this.lblIterations_TextChanged);
         // 
         // numThreshold
         // 
         this.numThreshold.Location = new System.Drawing.Point(151, 81);
         this.numThreshold.Name = "numThreshold";
         this.numThreshold.Size = new System.Drawing.Size(52, 20);
         this.numThreshold.TabIndex = 8;
         this.numThreshold.ValueChanged += new System.EventHandler(this.numThreshold_ValueChanged);
         // 
         // numUIDelay
         // 
         this.numUIDelay.Location = new System.Drawing.Point(270, 16);
         this.numUIDelay.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
         this.numUIDelay.Name = "numUIDelay";
         this.numUIDelay.Size = new System.Drawing.Size(46, 20);
         this.numUIDelay.TabIndex = 5;
         this.numUIDelay.ValueChanged += new System.EventHandler(this.numUIDelay_ValueChanged);
         // 
         // numThresholdHigh
         // 
         this.numThresholdHigh.Location = new System.Drawing.Point(209, 81);
         this.numThresholdHigh.Name = "numThresholdHigh";
         this.numThresholdHigh.Size = new System.Drawing.Size(52, 20);
         this.numThresholdHigh.TabIndex = 8;
         this.numThresholdHigh.ValueChanged += new System.EventHandler(this.numThresholdHigh_ValueChanged);
         // 
         // Control
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(352, 409);
         this.Controls.Add(this.numThresholdHigh);
         this.Controls.Add(this.numThreshold);
         this.Controls.Add(this.lblIterations);
         this.Controls.Add(this.chkRandomizeColors);
         this.Controls.Add(this.numStates);
         this.Controls.Add(this.numAgeSpeed);
         this.Controls.Add(this.numUIDelay);
         this.Controls.Add(this.numDelay);
         this.Controls.Add(this.lblDescription);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.lblWindowSize);
         this.Controls.Add(this.lblDimensions);
         this.Controls.Add(this.txtWindowSize);
         this.Controls.Add(this.txtDimensions);
         this.Controls.Add(this.cboxAgeEffect);
         this.Controls.Add(this.cboxRule);
         this.Controls.Add(this.cboxInitialCond);
         this.Controls.Add(this.btnExit);
         this.Controls.Add(this.btnSetWindowSize);
         this.Controls.Add(this.btnReset);
         this.Controls.Add(this.btnSetDimensions);
         this.Controls.Add(this.btnStartStop);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
         this.Name = "Control";
         this.Text = "Control";
         this.Activated += new System.EventHandler(this.Control_Activated);
         this.Shown += new System.EventHandler(this.Control_Shown);
         ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.numStates)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.numAgeSpeed)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.numThreshold)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.numUIDelay)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.numThresholdHigh)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button btnStartStop;
      private System.Windows.Forms.Button btnReset;
      private System.Windows.Forms.ComboBox cboxInitialCond;
      private System.Windows.Forms.Button btnExit;
      private System.Windows.Forms.TextBox txtDimensions;
      private System.Windows.Forms.Label lblDimensions;
      private System.Windows.Forms.Button btnSetDimensions;
      private System.Windows.Forms.Button btnSetWindowSize;
      private System.Windows.Forms.TextBox txtWindowSize;
      private System.Windows.Forms.Label lblWindowSize;
      private System.Windows.Forms.ComboBox cboxRule;
      private System.Windows.Forms.Label lblDescription;
      private System.Windows.Forms.NumericUpDown numDelay;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.NumericUpDown numStates;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.CheckBox chkRandomizeColors;
      private System.Windows.Forms.ComboBox cboxAgeEffect;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.NumericUpDown numAgeSpeed;
      private System.Windows.Forms.OpenFileDialog openFileDialog;
      public System.Windows.Forms.Label lblIterations;
      private System.Windows.Forms.NumericUpDown numThreshold;
      private System.Windows.Forms.NumericUpDown numUIDelay;
      private System.Windows.Forms.NumericUpDown numThresholdHigh;
   }
}