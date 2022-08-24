namespace ChaosGame {
    partial class GeneratePoints {
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabCircular = new System.Windows.Forms.TabPage();
            this.txtCenter = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numArcSweep = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numSpiralStep = new System.Windows.Forms.NumericUpDown();
            this.numRadius = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numCircularPoints = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numStartAngle = new System.Windows.Forms.NumericUpDown();
            this.tabRectangular = new System.Windows.Forms.TabPage();
            this.txtRectSize = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTopLeft = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numPointsPerSide = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkLeft = new System.Windows.Forms.CheckBox();
            this.chkRight = new System.Windows.Forms.CheckBox();
            this.chkBottom = new System.Windows.Forms.CheckBox();
            this.chkTop = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabPattern = new System.Windows.Forms.TabPage();
            this.txtPatternSize = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPatternCenter = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.numPatternPoints = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.cboxPattern = new System.Windows.Forms.ComboBox();
            this.tabFromImage = new System.Windows.Forms.TabPage();
            this.pboxImage = new MyPBox();
            this.btnRemoveImageFromList = new System.Windows.Forms.Button();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.lblPointsFound = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cboxImageFile = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblPointCount = new System.Windows.Forms.Label();
            this.btnCreatePoints = new System.Windows.Forms.Button();
            this.chkAddNewPoints = new System.Windows.Forms.CheckBox();
            this.tabControl.SuspendLayout();
            this.tabCircular.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numArcSweep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpiralStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCircularPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartAngle)).BeginInit();
            this.tabRectangular.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPointsPerSide)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabPattern.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPatternPoints)).BeginInit();
            this.tabFromImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabCircular);
            this.tabControl.Controls.Add(this.tabRectangular);
            this.tabControl.Controls.Add(this.tabPattern);
            this.tabControl.Controls.Add(this.tabFromImage);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(285, 323);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabCircular
            // 
            this.tabCircular.Controls.Add(this.txtCenter);
            this.tabCircular.Controls.Add(this.label3);
            this.tabCircular.Controls.Add(this.label5);
            this.tabCircular.Controls.Add(this.numArcSweep);
            this.tabCircular.Controls.Add(this.label6);
            this.tabCircular.Controls.Add(this.label4);
            this.tabCircular.Controls.Add(this.numSpiralStep);
            this.tabCircular.Controls.Add(this.numRadius);
            this.tabCircular.Controls.Add(this.label2);
            this.tabCircular.Controls.Add(this.numCircularPoints);
            this.tabCircular.Controls.Add(this.label1);
            this.tabCircular.Controls.Add(this.numStartAngle);
            this.tabCircular.Location = new System.Drawing.Point(4, 26);
            this.tabCircular.Name = "tabCircular";
            this.tabCircular.Padding = new System.Windows.Forms.Padding(3);
            this.tabCircular.Size = new System.Drawing.Size(277, 293);
            this.tabCircular.TabIndex = 0;
            this.tabCircular.Text = "Circular";
            this.tabCircular.UseVisualStyleBackColor = true;
            // 
            // txtCenter
            // 
            this.txtCenter.Location = new System.Drawing.Point(3, 115);
            this.txtCenter.Name = "txtCenter";
            this.txtCenter.Size = new System.Drawing.Size(109, 25);
            this.txtCenter.TabIndex = 2;
            this.txtCenter.TextChanged += new System.EventHandler(this.txtCenter_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Center (x, y)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(122, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "Arc sweep (deg.)";
            // 
            // numArcSweep
            // 
            this.numArcSweep.Location = new System.Drawing.Point(122, 69);
            this.numArcSweep.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numArcSweep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numArcSweep.Name = "numArcSweep";
            this.numArcSweep.Size = new System.Drawing.Size(110, 25);
            this.numArcSweep.TabIndex = 0;
            this.numArcSweep.Value = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numArcSweep.ValueChanged += new System.EventHandler(this.numArcSweep_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 17);
            this.label6.TabIndex = 1;
            this.label6.Text = "Step (spiral)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(122, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Radius";
            // 
            // numSpiralStep
            // 
            this.numSpiralStep.Location = new System.Drawing.Point(3, 161);
            this.numSpiralStep.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numSpiralStep.Name = "numSpiralStep";
            this.numSpiralStep.Size = new System.Drawing.Size(110, 25);
            this.numSpiralStep.TabIndex = 0;
            this.numSpiralStep.ValueChanged += new System.EventHandler(this.numSpiralStep_ValueChanged);
            // 
            // numRadius
            // 
            this.numRadius.Location = new System.Drawing.Point(122, 115);
            this.numRadius.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numRadius.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRadius.Name = "numRadius";
            this.numRadius.Size = new System.Drawing.Size(110, 25);
            this.numRadius.TabIndex = 0;
            this.numRadius.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numRadius.ValueChanged += new System.EventHandler(this.numRadius_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Number of points";
            // 
            // numCircularPoints
            // 
            this.numCircularPoints.Location = new System.Drawing.Point(3, 69);
            this.numCircularPoints.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numCircularPoints.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCircularPoints.Name = "numCircularPoints";
            this.numCircularPoints.Size = new System.Drawing.Size(109, 25);
            this.numCircularPoints.TabIndex = 0;
            this.numCircularPoints.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numCircularPoints.ValueChanged += new System.EventHandler(this.numCircularPoints_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Start angle (deg. from vertical)";
            // 
            // numStartAngle
            // 
            this.numStartAngle.Location = new System.Drawing.Point(3, 23);
            this.numStartAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numStartAngle.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.numStartAngle.Name = "numStartAngle";
            this.numStartAngle.Size = new System.Drawing.Size(109, 25);
            this.numStartAngle.TabIndex = 0;
            this.numStartAngle.ValueChanged += new System.EventHandler(this.numStartAngle_ValueChanged);
            // 
            // tabRectangular
            // 
            this.tabRectangular.Controls.Add(this.txtRectSize);
            this.tabRectangular.Controls.Add(this.label9);
            this.tabRectangular.Controls.Add(this.txtTopLeft);
            this.tabRectangular.Controls.Add(this.label8);
            this.tabRectangular.Controls.Add(this.label7);
            this.tabRectangular.Controls.Add(this.numPointsPerSide);
            this.tabRectangular.Controls.Add(this.panel2);
            this.tabRectangular.Location = new System.Drawing.Point(4, 26);
            this.tabRectangular.Name = "tabRectangular";
            this.tabRectangular.Padding = new System.Windows.Forms.Padding(3);
            this.tabRectangular.Size = new System.Drawing.Size(277, 293);
            this.tabRectangular.TabIndex = 1;
            this.tabRectangular.Text = "Rectangular";
            this.tabRectangular.UseVisualStyleBackColor = true;
            // 
            // txtRectSize
            // 
            this.txtRectSize.Location = new System.Drawing.Point(122, 69);
            this.txtRectSize.Name = "txtRectSize";
            this.txtRectSize.Size = new System.Drawing.Size(109, 25);
            this.txtRectSize.TabIndex = 6;
            this.txtRectSize.TextChanged += new System.EventHandler(this.txtRectSize_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(122, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 17);
            this.label9.TabIndex = 5;
            this.label9.Text = "Size (h, w)";
            // 
            // txtTopLeft
            // 
            this.txtTopLeft.Location = new System.Drawing.Point(3, 69);
            this.txtTopLeft.Name = "txtTopLeft";
            this.txtTopLeft.Size = new System.Drawing.Size(109, 25);
            this.txtTopLeft.TabIndex = 6;
            this.txtTopLeft.TextChanged += new System.EventHandler(this.txtTopLeft_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 17);
            this.label8.TabIndex = 5;
            this.label8.Text = "Top left (x, y)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "Points per side";
            // 
            // numPointsPerSide
            // 
            this.numPointsPerSide.Location = new System.Drawing.Point(3, 23);
            this.numPointsPerSide.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numPointsPerSide.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPointsPerSide.Name = "numPointsPerSide";
            this.numPointsPerSide.Size = new System.Drawing.Size(109, 25);
            this.numPointsPerSide.TabIndex = 0;
            this.numPointsPerSide.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPointsPerSide.ValueChanged += new System.EventHandler(this.numPointsPerSide_ValueChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkLeft);
            this.panel2.Controls.Add(this.chkRight);
            this.panel2.Controls.Add(this.chkBottom);
            this.panel2.Controls.Add(this.chkTop);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(231, 120);
            this.panel2.TabIndex = 8;
            // 
            // chkLeft
            // 
            this.chkLeft.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkLeft.AutoSize = true;
            this.chkLeft.Checked = true;
            this.chkLeft.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLeft.Location = new System.Drawing.Point(12, 31);
            this.chkLeft.Name = "chkLeft";
            this.chkLeft.Size = new System.Drawing.Size(48, 21);
            this.chkLeft.TabIndex = 7;
            this.chkLeft.Text = "Left";
            this.chkLeft.UseVisualStyleBackColor = true;
            this.chkLeft.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkRight
            // 
            this.chkRight.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkRight.AutoSize = true;
            this.chkRight.Checked = true;
            this.chkRight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRight.Location = new System.Drawing.Point(177, 31);
            this.chkRight.Name = "chkRight";
            this.chkRight.Size = new System.Drawing.Size(57, 21);
            this.chkRight.TabIndex = 7;
            this.chkRight.Text = "Right";
            this.chkRight.UseVisualStyleBackColor = true;
            this.chkRight.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkBottom
            // 
            this.chkBottom.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.chkBottom.AutoSize = true;
            this.chkBottom.Checked = true;
            this.chkBottom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBottom.Location = new System.Drawing.Point(81, 62);
            this.chkBottom.Name = "chkBottom";
            this.chkBottom.Size = new System.Drawing.Size(69, 21);
            this.chkBottom.TabIndex = 7;
            this.chkBottom.Text = "Bottom";
            this.chkBottom.UseVisualStyleBackColor = true;
            this.chkBottom.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkTop
            // 
            this.chkTop.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkTop.AutoSize = true;
            this.chkTop.Checked = true;
            this.chkTop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTop.Location = new System.Drawing.Point(91, 1);
            this.chkTop.Name = "chkTop";
            this.chkTop.Size = new System.Drawing.Size(49, 21);
            this.chkTop.TabIndex = 7;
            this.chkTop.Text = "Top";
            this.chkTop.UseVisualStyleBackColor = true;
            this.chkTop.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(30, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(173, 62);
            this.panel1.TabIndex = 8;
            // 
            // tabPattern
            // 
            this.tabPattern.Controls.Add(this.txtPatternSize);
            this.tabPattern.Controls.Add(this.label13);
            this.tabPattern.Controls.Add(this.txtPatternCenter);
            this.tabPattern.Controls.Add(this.label12);
            this.tabPattern.Controls.Add(this.label11);
            this.tabPattern.Controls.Add(this.numPatternPoints);
            this.tabPattern.Controls.Add(this.label10);
            this.tabPattern.Controls.Add(this.cboxPattern);
            this.tabPattern.Location = new System.Drawing.Point(4, 26);
            this.tabPattern.Name = "tabPattern";
            this.tabPattern.Padding = new System.Windows.Forms.Padding(3);
            this.tabPattern.Size = new System.Drawing.Size(277, 293);
            this.tabPattern.TabIndex = 2;
            this.tabPattern.Text = "Pattern";
            this.tabPattern.UseVisualStyleBackColor = true;
            // 
            // txtPatternSize
            // 
            this.txtPatternSize.Location = new System.Drawing.Point(128, 115);
            this.txtPatternSize.Name = "txtPatternSize";
            this.txtPatternSize.Size = new System.Drawing.Size(109, 25);
            this.txtPatternSize.TabIndex = 11;
            this.txtPatternSize.TextChanged += new System.EventHandler(this.txtPatternSize_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(126, 97);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 17);
            this.label13.TabIndex = 10;
            this.label13.Text = "Size (h, w)";
            // 
            // txtPatternCenter
            // 
            this.txtPatternCenter.Location = new System.Drawing.Point(3, 115);
            this.txtPatternCenter.Name = "txtPatternCenter";
            this.txtPatternCenter.Size = new System.Drawing.Size(109, 25);
            this.txtPatternCenter.TabIndex = 9;
            this.txtPatternCenter.TextChanged += new System.EventHandler(this.txtPatternCenter_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 97);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 17);
            this.label12.TabIndex = 8;
            this.label12.Text = "Center (x, y)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(121, 17);
            this.label11.TabIndex = 7;
            this.label11.Text = "Points per segment";
            // 
            // numPatternPoints
            // 
            this.numPatternPoints.Location = new System.Drawing.Point(3, 69);
            this.numPatternPoints.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numPatternPoints.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPatternPoints.Name = "numPatternPoints";
            this.numPatternPoints.Size = new System.Drawing.Size(109, 25);
            this.numPatternPoints.TabIndex = 6;
            this.numPatternPoints.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPatternPoints.ValueChanged += new System.EventHandler(this.numPatternPoints_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 17);
            this.label10.TabIndex = 1;
            this.label10.Text = "Pattern";
            // 
            // cboxPattern
            // 
            this.cboxPattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxPattern.FormattingEnabled = true;
            this.cboxPattern.Items.AddRange(new object[] {
            "Star"});
            this.cboxPattern.Location = new System.Drawing.Point(3, 23);
            this.cboxPattern.Name = "cboxPattern";
            this.cboxPattern.Size = new System.Drawing.Size(165, 25);
            this.cboxPattern.TabIndex = 0;
            this.cboxPattern.SelectedIndexChanged += new System.EventHandler(this.cboxPattern_SelectedIndexChanged);
            // 
            // tabFromImage
            // 
            this.tabFromImage.Controls.Add(this.pboxImage);
            this.tabFromImage.Controls.Add(this.btnRemoveImageFromList);
            this.tabFromImage.Controls.Add(this.btnLoadImage);
            this.tabFromImage.Controls.Add(this.lblPointsFound);
            this.tabFromImage.Controls.Add(this.label14);
            this.tabFromImage.Controls.Add(this.cboxImageFile);
            this.tabFromImage.Location = new System.Drawing.Point(4, 26);
            this.tabFromImage.Name = "tabFromImage";
            this.tabFromImage.Padding = new System.Windows.Forms.Padding(3);
            this.tabFromImage.Size = new System.Drawing.Size(277, 293);
            this.tabFromImage.TabIndex = 3;
            this.tabFromImage.Text = "From image";
            this.tabFromImage.UseVisualStyleBackColor = true;
            // 
            // pboxImage
            // 
            this.pboxImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pboxImage.BackColor = System.Drawing.Color.Gainsboro;
            this.pboxImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pboxImage.Location = new System.Drawing.Point(3, 104);
            this.pboxImage.Name = "pboxImage";
            this.pboxImage.Size = new System.Drawing.Size(268, 186);
            this.pboxImage.TabIndex = 3;
            this.pboxImage.TabStop = false;
            // 
            // btnRemoveImageFromList
            // 
            this.btnRemoveImageFromList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveImageFromList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveImageFromList.Location = new System.Drawing.Point(246, 54);
            this.btnRemoveImageFromList.Name = "btnRemoveImageFromList";
            this.btnRemoveImageFromList.Size = new System.Drawing.Size(25, 25);
            this.btnRemoveImageFromList.TabIndex = 2;
            this.btnRemoveImageFromList.Text = "X";
            this.btnRemoveImageFromList.UseVisualStyleBackColor = true;
            this.btnRemoveImageFromList.Click += new System.EventHandler(this.btnRemoveImageFromList_Click);
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(3, 54);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(102, 31);
            this.btnLoadImage.TabIndex = 2;
            this.btnLoadImage.Text = "Load image...";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // lblPointsFound
            // 
            this.lblPointsFound.AutoSize = true;
            this.lblPointsFound.Location = new System.Drawing.Point(3, 85);
            this.lblPointsFound.Name = "lblPointsFound";
            this.lblPointsFound.Size = new System.Drawing.Size(95, 17);
            this.lblPointsFound.TabIndex = 1;
            this.lblPointsFound.Text = "Points found: 0";
            this.lblPointsFound.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 5);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 17);
            this.label14.TabIndex = 1;
            this.label14.Text = "Image file";
            // 
            // cboxImageFile
            // 
            this.cboxImageFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxImageFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxImageFile.FormattingEnabled = true;
            this.cboxImageFile.Location = new System.Drawing.Point(3, 23);
            this.cboxImageFile.Name = "cboxImageFile";
            this.cboxImageFile.Size = new System.Drawing.Size(268, 25);
            this.cboxImageFile.TabIndex = 0;
            this.cboxImageFile.SelectedIndexChanged += new System.EventHandler(this.cboxImageFile_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(180, 395);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 31);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(86, 395);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(87, 31);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // lblPointCount
            // 
            this.lblPointCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPointCount.AutoSize = true;
            this.lblPointCount.Location = new System.Drawing.Point(166, 328);
            this.lblPointCount.Name = "lblPointCount";
            this.lblPointCount.Size = new System.Drawing.Size(105, 17);
            this.lblPointCount.TabIndex = 2;
            this.lblPointCount.Text = "Points created: 0";
            // 
            // btnCreatePoints
            // 
            this.btnCreatePoints.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreatePoints.Location = new System.Drawing.Point(14, 348);
            this.btnCreatePoints.Name = "btnCreatePoints";
            this.btnCreatePoints.Size = new System.Drawing.Size(254, 37);
            this.btnCreatePoints.TabIndex = 5;
            this.btnCreatePoints.Text = "Create points";
            this.btnCreatePoints.UseVisualStyleBackColor = true;
            this.btnCreatePoints.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // chkAddNewPoints
            // 
            this.chkAddNewPoints.AutoSize = true;
            this.chkAddNewPoints.Location = new System.Drawing.Point(14, 327);
            this.chkAddNewPoints.Name = "chkAddNewPoints";
            this.chkAddNewPoints.Size = new System.Drawing.Size(115, 21);
            this.chkAddNewPoints.TabIndex = 6;
            this.chkAddNewPoints.Text = "Add to existing";
            this.chkAddNewPoints.UseVisualStyleBackColor = true;
            this.chkAddNewPoints.CheckedChanged += new System.EventHandler(this.chkAddNewPoints_CheckedChanged);
            // 
            // GeneratePoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 440);
            this.Controls.Add(this.chkAddNewPoints);
            this.Controls.Add(this.lblPointCount);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnCreatePoints);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GeneratePoints";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generate points";
            this.tabControl.ResumeLayout(false);
            this.tabCircular.ResumeLayout(false);
            this.tabCircular.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numArcSweep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpiralStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCircularPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartAngle)).EndInit();
            this.tabRectangular.ResumeLayout(false);
            this.tabRectangular.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPointsPerSide)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPattern.ResumeLayout(false);
            this.tabPattern.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPatternPoints)).EndInit();
            this.tabFromImage.ResumeLayout(false);
            this.tabFromImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabCircular;
        private System.Windows.Forms.TextBox txtCenter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numArcSweep;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numSpiralStep;
        private System.Windows.Forms.NumericUpDown numRadius;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numCircularPoints;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numStartAngle;
        private System.Windows.Forms.TabPage tabRectangular;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblPointCount;
        private System.Windows.Forms.NumericUpDown numPointsPerSide;
        private System.Windows.Forms.CheckBox chkBottom;
        private System.Windows.Forms.CheckBox chkRight;
        private System.Windows.Forms.CheckBox chkLeft;
        private System.Windows.Forms.CheckBox chkTop;
        private System.Windows.Forms.TextBox txtRectSize;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTopLeft;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabPattern;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboxPattern;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numPatternPoints;
        private System.Windows.Forms.TextBox txtPatternCenter;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPatternSize;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnCreatePoints;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabPage tabFromImage;
        private MyPBox pboxImage;
        private System.Windows.Forms.Button btnRemoveImageFromList;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboxImageFile;
        private System.Windows.Forms.Label lblPointsFound;
        private System.Windows.Forms.CheckBox chkAddNewPoints;
    }
}