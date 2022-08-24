namespace ChaosGame {
    partial class MainWindow {
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
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.lblIterations = new System.Windows.Forms.Label();
            this.lblPoints = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.numSize = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numMargin = new System.Windows.Forms.NumericUpDown();
            this.btnPlot = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.chkHilitePoints = new System.Windows.Forms.CheckBox();
            this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnAlgorithmicSettings = new System.Windows.Forms.Button();
            this.lblOperSettings = new System.Windows.Forms.Label();
            this.lblDimensions = new System.Windows.Forms.Label();
            this.timerHideDimensions = new System.Windows.Forms.Timer(this.components);
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pointGamePanel = new PointGamePanel();
            ((System.ComponentModel.ISupportInitialize)(this.numSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMargin)).BeginInit();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 300;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // lblIterations
            // 
            this.lblIterations.AutoSize = true;
            this.lblIterations.Location = new System.Drawing.Point(389, 28);
            this.lblIterations.Name = "lblIterations";
            this.lblIterations.Size = new System.Drawing.Size(62, 13);
            this.lblIterations.TabIndex = 2;
            this.lblIterations.Text = "Iterations: 0";
            // 
            // lblPoints
            // 
            this.lblPoints.AutoSize = true;
            this.lblPoints.Location = new System.Drawing.Point(389, 12);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(48, 13);
            this.lblPoints.TabIndex = 2;
            this.lblPoints.Text = "Points: 0";
            // 
            // btnGo
            // 
            this.btnGo.Enabled = false;
            this.btnGo.Location = new System.Drawing.Point(151, 12);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(164, 23);
            this.btnGo.TabIndex = 0;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // numSize
            // 
            this.numSize.Location = new System.Drawing.Point(106, 41);
            this.numSize.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numSize.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numSize.Name = "numSize";
            this.numSize.Size = new System.Drawing.Size(75, 20);
            this.numSize.TabIndex = 6;
            this.numSize.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numSize.ValueChanged += new System.EventHandler(this.numSize_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Size:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(187, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Margin:";
            // 
            // numMargin
            // 
            this.numMargin.Location = new System.Drawing.Point(235, 41);
            this.numMargin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMargin.Name = "numMargin";
            this.numMargin.Size = new System.Drawing.Size(75, 20);
            this.numMargin.TabIndex = 6;
            this.numMargin.ValueChanged += new System.EventHandler(this.numMargin_ValueChanged);
            // 
            // btnPlot
            // 
            this.btnPlot.Location = new System.Drawing.Point(321, 12);
            this.btnPlot.Name = "btnPlot";
            this.btnPlot.Size = new System.Drawing.Size(62, 49);
            this.btnPlot.TabIndex = 7;
            this.btnPlot.Text = "Generate points";
            this.btnPlot.UseVisualStyleBackColor = true;
            this.btnPlot.Click += new System.EventHandler(this.btnPlot_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(70, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // chkHilitePoints
            // 
            this.chkHilitePoints.AutoSize = true;
            this.chkHilitePoints.Location = new System.Drawing.Point(392, 44);
            this.chkHilitePoints.Name = "chkHilitePoints";
            this.chkHilitePoints.Size = new System.Drawing.Size(80, 17);
            this.chkHilitePoints.TabIndex = 8;
            this.chkHilitePoints.Text = "Hilite points";
            this.chkHilitePoints.UseVisualStyleBackColor = true;
            this.chkHilitePoints.CheckedChanged += new System.EventHandler(this.chkHilitePoints_CheckedChanged);
            // 
            // saveImageDialog
            // 
            this.saveImageDialog.DefaultExt = "png";
            // 
            // btnAlgorithmicSettings
            // 
            this.btnAlgorithmicSettings.Location = new System.Drawing.Point(518, 12);
            this.btnAlgorithmicSettings.Name = "btnAlgorithmicSettings";
            this.btnAlgorithmicSettings.Size = new System.Drawing.Size(72, 49);
            this.btnAlgorithmicSettings.TabIndex = 10;
            this.btnAlgorithmicSettings.Text = "Algorithmic settings";
            this.btnAlgorithmicSettings.UseVisualStyleBackColor = true;
            this.btnAlgorithmicSettings.Click += new System.EventHandler(this.btnAlgorithmicSettings_Click);
            // 
            // lblOperSettings
            // 
            this.lblOperSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOperSettings.Location = new System.Drawing.Point(596, 12);
            this.lblOperSettings.Name = "lblOperSettings";
            this.lblOperSettings.Size = new System.Drawing.Size(277, 49);
            this.lblOperSettings.TabIndex = 11;
            this.lblOperSettings.Text = "Algorithmic settings show here.";
            // 
            // lblDimensions
            // 
            this.lblDimensions.AutoSize = true;
            this.lblDimensions.Location = new System.Drawing.Point(12, 64);
            this.lblDimensions.Name = "lblDimensions";
            this.lblDimensions.Size = new System.Drawing.Size(35, 13);
            this.lblDimensions.TabIndex = 12;
            this.lblDimensions.Text = "label1";
            this.lblDimensions.Visible = false;
            // 
            // timerHideDimensions
            // 
            this.timerHideDimensions.Interval = 1000;
            this.timerHideDimensions.Tick += new System.EventHandler(this.timerHideDimensions_Tick);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(12, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(52, 23);
            this.btnLoad.TabIndex = 13;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 38);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(52, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pointGamePanel
            // 
            this.pointGamePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pointGamePanel.Bmp = null;
            this.pointGamePanel.CircularPath = false;
            this.pointGamePanel.ComboMix = -1;
            this.pointGamePanel.ConstantDist = 0F;
            this.pointGamePanel.DistMeaning = PointGamePanel.DistanceMeaning.FractionDistToTarget;
            this.pointGamePanel.DrawLines = false;
            this.pointGamePanel.HilitePoints = false;
            this.pointGamePanel.IncludeCurPutInMoveCalc = false;
            this.pointGamePanel.Location = new System.Drawing.Point(12, 64);
            this.pointGamePanel.MaxIterations = ((ulong)(0ul));
            this.pointGamePanel.MixColors = false;
            this.pointGamePanel.MoveN = 0;
            this.pointGamePanel.Name = "pointGamePanel";
            this.pointGamePanel.PickN = 0;
            this.pointGamePanel.PickOneMovement = PointGamePanel.OnePointMovement.DirectToPoint;
            this.pointGamePanel.PickOneSelection = PointGamePanel.OnePointSelection.Random;
            this.pointGamePanel.PickRadius = 0D;
            this.pointGamePanel.PickTwoMovement = PointGamePanel.TwoPointMovement.None;
            this.pointGamePanel.PickTwoSelection = PointGamePanel.TwoPointSelection.Random;
            this.pointGamePanel.PtSelection = PointGamePanel.PointSelection.PickOne;
            this.pointGamePanel.Running = false;
            this.pointGamePanel.Size = new System.Drawing.Size(861, 861);
            this.pointGamePanel.TabIndex = 3;
            this.pointGamePanel.TravelDist = 0.5F;
            this.pointGamePanel.PointsCreated += new System.EventHandler(this.pointGamePanel_PointsCreated);
            this.pointGamePanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pointGamePanel_MouseUp);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 937);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.lblDimensions);
            this.Controls.Add(this.lblOperSettings);
            this.Controls.Add(this.btnAlgorithmicSettings);
            this.Controls.Add(this.chkHilitePoints);
            this.Controls.Add(this.btnPlot);
            this.Controls.Add(this.numMargin);
            this.Controls.Add(this.numSize);
            this.Controls.Add(this.pointGamePanel);
            this.Controls.Add(this.lblIterations);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblPoints);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.btnClear);
            this.Name = "MainWindow";
            this.Text = "Point Game Explorer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMargin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PointGamePanel pointGamePanel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label lblIterations;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.NumericUpDown numSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numMargin;
        private System.Windows.Forms.Button btnPlot;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox chkHilitePoints;
        private System.Windows.Forms.SaveFileDialog saveImageDialog;
        private System.Windows.Forms.Button btnAlgorithmicSettings;
        private System.Windows.Forms.Label lblOperSettings;
        private System.Windows.Forms.Label lblDimensions;
        private System.Windows.Forms.Timer timerHideDimensions;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
    }
}

