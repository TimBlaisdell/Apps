
namespace ChaosGame {
    partial class AlgorithmicSettings {
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
            this.cboxTwoPtMethod = new System.Windows.Forms.ComboBox();
            this.rbtnOnePoint = new System.Windows.Forms.RadioButton();
            this.rbtnTwoPoints = new System.Windows.Forms.RadioButton();
            this.cboxOnePointMethod = new System.Windows.Forms.ComboBox();
            this.txtTravelDist = new System.Windows.Forms.TextBox();
            this.chkDrawLines = new System.Windows.Forms.CheckBox();
            this.chkRandomizeBlack = new System.Windows.Forms.CheckBox();
            this.chkMixColors = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cboxOnePointMove = new System.Windows.Forms.ComboBox();
            this.cboxTwoPointMove = new System.Windows.Forms.ComboBox();
            this.numRadius = new System.Windows.Forms.NumericUpDown();
            this.numPickN = new System.Windows.Forms.NumericUpDown();
            this.numMoveN = new System.Windows.Forms.NumericUpDown();
            this.chkIncludeCurPnt = new System.Windows.Forms.CheckBox();
            this.rbtnFractionOfDist = new System.Windows.Forms.RadioButton();
            this.rbtnFractionOfMaxDist = new System.Windows.Forms.RadioButton();
            this.rbtnFractionOfMinDist = new System.Windows.Forms.RadioButton();
            this.rbtnConstantDist = new System.Windows.Forms.RadioButton();
            this.groupDistanceMeaning = new System.Windows.Forms.GroupBox();
            this.chkCombineDist = new System.Windows.Forms.CheckBox();
            this.sbarComboMix = new System.Windows.Forms.HScrollBar();
            this.lblPctDist2 = new System.Windows.Forms.Label();
            this.lblPctDist1 = new System.Windows.Forms.Label();
            this.rbtnComboDist = new System.Windows.Forms.RadioButton();
            this.txtConstDist = new System.Windows.Forms.TextBox();
            this.numMaxIterations = new System.Windows.Forms.NumericUpDown();
            this.chkMaxIterations = new System.Windows.Forms.CheckBox();
            this.chkAlongCircle = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPickN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMoveN)).BeginInit();
            this.groupDistanceMeaning.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxIterations)).BeginInit();
            this.SuspendLayout();
            // 
            // cboxTwoPtMethod
            // 
            this.cboxTwoPtMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxTwoPtMethod.FormattingEnabled = true;
            this.cboxTwoPtMethod.Items.AddRange(new object[] {
            "Move toward midpoint",
            "Spider-man method"});
            this.cboxTwoPtMethod.Location = new System.Drawing.Point(195, 38);
            this.cboxTwoPtMethod.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboxTwoPtMethod.Name = "cboxTwoPtMethod";
            this.cboxTwoPtMethod.Size = new System.Drawing.Size(174, 25);
            this.cboxTwoPtMethod.TabIndex = 10;
            this.cboxTwoPtMethod.Visible = false;
            this.cboxTwoPtMethod.SelectedIndexChanged += new System.EventHandler(this.cboxTwoPtMethod_SelectedIndexChanged);
            // 
            // rbtnOnePoint
            // 
            this.rbtnOnePoint.AutoSize = true;
            this.rbtnOnePoint.Location = new System.Drawing.Point(12, 13);
            this.rbtnOnePoint.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbtnOnePoint.Name = "rbtnOnePoint";
            this.rbtnOnePoint.Size = new System.Drawing.Size(108, 21);
            this.rbtnOnePoint.TabIndex = 11;
            this.rbtnOnePoint.Text = "Pick one point";
            this.rbtnOnePoint.UseVisualStyleBackColor = true;
            this.rbtnOnePoint.CheckedChanged += new System.EventHandler(this.rbtnOnePoint_CheckedChanged);
            // 
            // rbtnTwoPoints
            // 
            this.rbtnTwoPoints.AutoSize = true;
            this.rbtnTwoPoints.Location = new System.Drawing.Point(195, 13);
            this.rbtnTwoPoints.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbtnTwoPoints.Name = "rbtnTwoPoints";
            this.rbtnTwoPoints.Size = new System.Drawing.Size(113, 21);
            this.rbtnTwoPoints.TabIndex = 11;
            this.rbtnTwoPoints.Text = "Pick two points";
            this.rbtnTwoPoints.UseVisualStyleBackColor = true;
            this.rbtnTwoPoints.CheckedChanged += new System.EventHandler(this.rbtnTwoPoints_CheckedChanged);
            // 
            // cboxOnePointMethod
            // 
            this.cboxOnePointMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxOnePointMethod.FormattingEnabled = true;
            this.cboxOnePointMethod.Items.AddRange(new object[] {
            "Pick at random",
            "Pick among N closest",
            "Pick among N farthest",
            "Pick within limited radius"});
            this.cboxOnePointMethod.Location = new System.Drawing.Point(12, 38);
            this.cboxOnePointMethod.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboxOnePointMethod.Name = "cboxOnePointMethod";
            this.cboxOnePointMethod.Size = new System.Drawing.Size(174, 25);
            this.cboxOnePointMethod.TabIndex = 10;
            this.cboxOnePointMethod.Visible = false;
            this.cboxOnePointMethod.SelectedIndexChanged += new System.EventHandler(this.cboxOnePointMethod_SelectedIndexChanged);
            // 
            // txtTravelDist
            // 
            this.txtTravelDist.Location = new System.Drawing.Point(13, 121);
            this.txtTravelDist.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTravelDist.Name = "txtTravelDist";
            this.txtTravelDist.Size = new System.Drawing.Size(89, 25);
            this.txtTravelDist.TabIndex = 16;
            this.txtTravelDist.Text = "0.5";
            this.txtTravelDist.TextChanged += new System.EventHandler(this.txtTravelDist_TextChanged);
            // 
            // chkDrawLines
            // 
            this.chkDrawLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkDrawLines.AutoSize = true;
            this.chkDrawLines.Location = new System.Drawing.Point(12, 398);
            this.chkDrawLines.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkDrawLines.Name = "chkDrawLines";
            this.chkDrawLines.Size = new System.Drawing.Size(87, 21);
            this.chkDrawLines.TabIndex = 13;
            this.chkDrawLines.Text = "Draw lines";
            this.chkDrawLines.UseVisualStyleBackColor = true;
            this.chkDrawLines.CheckedChanged += new System.EventHandler(this.chkDrawLines_CheckedChanged);
            // 
            // chkRandomizeBlack
            // 
            this.chkRandomizeBlack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkRandomizeBlack.AutoSize = true;
            this.chkRandomizeBlack.Checked = true;
            this.chkRandomizeBlack.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRandomizeBlack.Location = new System.Drawing.Point(12, 350);
            this.chkRandomizeBlack.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkRandomizeBlack.Name = "chkRandomizeBlack";
            this.chkRandomizeBlack.Size = new System.Drawing.Size(125, 21);
            this.chkRandomizeBlack.TabIndex = 14;
            this.chkRandomizeBlack.Text = "Randomize Black";
            this.chkRandomizeBlack.UseVisualStyleBackColor = true;
            this.chkRandomizeBlack.CheckedChanged += new System.EventHandler(this.chkRandomizeBlack_CheckedChanged);
            // 
            // chkMixColors
            // 
            this.chkMixColors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkMixColors.AutoSize = true;
            this.chkMixColors.Checked = true;
            this.chkMixColors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMixColors.Location = new System.Drawing.Point(12, 374);
            this.chkMixColors.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkMixColors.Name = "chkMixColors";
            this.chkMixColors.Size = new System.Drawing.Size(88, 21);
            this.chkMixColors.TabIndex = 15;
            this.chkMixColors.Text = "Mix colors";
            this.chkMixColors.UseVisualStyleBackColor = true;
            this.chkMixColors.CheckedChanged += new System.EventHandler(this.chkMixColors_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Distance";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(186, 391);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(87, 30);
            this.btnOk.TabIndex = 17;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(280, 391);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 30);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cboxOnePointMove
            // 
            this.cboxOnePointMove.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxOnePointMove.FormattingEnabled = true;
            this.cboxOnePointMove.Location = new System.Drawing.Point(12, 71);
            this.cboxOnePointMove.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboxOnePointMove.Name = "cboxOnePointMove";
            this.cboxOnePointMove.Size = new System.Drawing.Size(174, 25);
            this.cboxOnePointMove.TabIndex = 10;
            this.cboxOnePointMove.Visible = false;
            this.cboxOnePointMove.SelectedIndexChanged += new System.EventHandler(this.cboxOnePointMove_SelectedIndexChanged);
            // 
            // cboxTwoPointMove
            // 
            this.cboxTwoPointMove.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxTwoPointMove.FormattingEnabled = true;
            this.cboxTwoPointMove.Items.AddRange(new object[] {
            "Move toward midpoint",
            "Spider-man method"});
            this.cboxTwoPointMove.Location = new System.Drawing.Point(195, 71);
            this.cboxTwoPointMove.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboxTwoPointMove.Name = "cboxTwoPointMove";
            this.cboxTwoPointMove.Size = new System.Drawing.Size(174, 25);
            this.cboxTwoPointMove.TabIndex = 10;
            this.cboxTwoPointMove.Visible = false;
            this.cboxTwoPointMove.SelectedIndexChanged += new System.EventHandler(this.cboxTwoPointMove_SelectedIndexChanged);
            // 
            // numRadius
            // 
            this.numRadius.Location = new System.Drawing.Point(152, 97);
            this.numRadius.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numRadius.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numRadius.Name = "numRadius";
            this.numRadius.Size = new System.Drawing.Size(58, 25);
            this.numRadius.TabIndex = 19;
            this.numRadius.Visible = false;
            this.numRadius.ValueChanged += new System.EventHandler(this.numRadius_ValueChanged);
            // 
            // numPickN
            // 
            this.numPickN.Location = new System.Drawing.Point(216, 97);
            this.numPickN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numPickN.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numPickN.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPickN.Name = "numPickN";
            this.numPickN.Size = new System.Drawing.Size(58, 25);
            this.numPickN.TabIndex = 19;
            this.numPickN.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPickN.Visible = false;
            this.numPickN.ValueChanged += new System.EventHandler(this.numPickN_ValueChanged);
            // 
            // numMoveN
            // 
            this.numMoveN.Location = new System.Drawing.Point(280, 97);
            this.numMoveN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numMoveN.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numMoveN.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMoveN.Name = "numMoveN";
            this.numMoveN.Size = new System.Drawing.Size(58, 25);
            this.numMoveN.TabIndex = 19;
            this.numMoveN.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMoveN.Visible = false;
            this.numMoveN.ValueChanged += new System.EventHandler(this.numMoveN_ValueChanged);
            // 
            // chkIncludeCurPnt
            // 
            this.chkIncludeCurPnt.AutoSize = true;
            this.chkIncludeCurPnt.Location = new System.Drawing.Point(223, 121);
            this.chkIncludeCurPnt.Name = "chkIncludeCurPnt";
            this.chkIncludeCurPnt.Size = new System.Drawing.Size(115, 21);
            this.chkIncludeCurPnt.TabIndex = 20;
            this.chkIncludeCurPnt.Text = "+ current point";
            this.chkIncludeCurPnt.UseVisualStyleBackColor = true;
            this.chkIncludeCurPnt.Visible = false;
            this.chkIncludeCurPnt.CheckedChanged += new System.EventHandler(this.chkIncludeCurPnt_CheckedChanged);
            // 
            // rbtnFractionOfDist
            // 
            this.rbtnFractionOfDist.AutoSize = true;
            this.rbtnFractionOfDist.Checked = true;
            this.rbtnFractionOfDist.Location = new System.Drawing.Point(6, 24);
            this.rbtnFractionOfDist.Name = "rbtnFractionOfDist";
            this.rbtnFractionOfDist.Size = new System.Drawing.Size(240, 21);
            this.rbtnFractionOfDist.TabIndex = 21;
            this.rbtnFractionOfDist.TabStop = true;
            this.rbtnFractionOfDist.Text = "fraction of distance to selected point";
            this.rbtnFractionOfDist.UseVisualStyleBackColor = true;
            this.rbtnFractionOfDist.CheckedChanged += new System.EventHandler(this.rbtnDist_CheckedChanged);
            // 
            // rbtnFractionOfMaxDist
            // 
            this.rbtnFractionOfMaxDist.AutoSize = true;
            this.rbtnFractionOfMaxDist.Location = new System.Drawing.Point(7, 45);
            this.rbtnFractionOfMaxDist.Name = "rbtnFractionOfMaxDist";
            this.rbtnFractionOfMaxDist.Size = new System.Drawing.Size(259, 21);
            this.rbtnFractionOfMaxDist.TabIndex = 21;
            this.rbtnFractionOfMaxDist.Text = "fraction of max distance between points";
            this.rbtnFractionOfMaxDist.UseVisualStyleBackColor = true;
            this.rbtnFractionOfMaxDist.CheckedChanged += new System.EventHandler(this.rbtnDist_CheckedChanged);
            // 
            // rbtnFractionOfMinDist
            // 
            this.rbtnFractionOfMinDist.AutoSize = true;
            this.rbtnFractionOfMinDist.Location = new System.Drawing.Point(6, 66);
            this.rbtnFractionOfMinDist.Name = "rbtnFractionOfMinDist";
            this.rbtnFractionOfMinDist.Size = new System.Drawing.Size(256, 21);
            this.rbtnFractionOfMinDist.TabIndex = 21;
            this.rbtnFractionOfMinDist.Text = "fraction of min distance between points";
            this.rbtnFractionOfMinDist.UseVisualStyleBackColor = true;
            this.rbtnFractionOfMinDist.CheckedChanged += new System.EventHandler(this.rbtnDist_CheckedChanged);
            // 
            // rbtnConstantDist
            // 
            this.rbtnConstantDist.AutoSize = true;
            this.rbtnConstantDist.Location = new System.Drawing.Point(6, 87);
            this.rbtnConstantDist.Name = "rbtnConstantDist";
            this.rbtnConstantDist.Size = new System.Drawing.Size(172, 21);
            this.rbtnConstantDist.TabIndex = 21;
            this.rbtnConstantDist.Text = "constant distance (pixels)";
            this.rbtnConstantDist.UseVisualStyleBackColor = true;
            this.rbtnConstantDist.CheckedChanged += new System.EventHandler(this.rbtnDist_CheckedChanged);
            // 
            // groupDistanceMeaning
            // 
            this.groupDistanceMeaning.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupDistanceMeaning.Controls.Add(this.chkCombineDist);
            this.groupDistanceMeaning.Controls.Add(this.sbarComboMix);
            this.groupDistanceMeaning.Controls.Add(this.lblPctDist2);
            this.groupDistanceMeaning.Controls.Add(this.lblPctDist1);
            this.groupDistanceMeaning.Controls.Add(this.rbtnFractionOfDist);
            this.groupDistanceMeaning.Controls.Add(this.rbtnFractionOfMaxDist);
            this.groupDistanceMeaning.Controls.Add(this.rbtnFractionOfMinDist);
            this.groupDistanceMeaning.Controls.Add(this.rbtnComboDist);
            this.groupDistanceMeaning.Controls.Add(this.rbtnConstantDist);
            this.groupDistanceMeaning.Controls.Add(this.txtConstDist);
            this.groupDistanceMeaning.Location = new System.Drawing.Point(12, 153);
            this.groupDistanceMeaning.Name = "groupDistanceMeaning";
            this.groupDistanceMeaning.Size = new System.Drawing.Size(358, 177);
            this.groupDistanceMeaning.TabIndex = 22;
            this.groupDistanceMeaning.TabStop = false;
            this.groupDistanceMeaning.Text = "Distance represents...";
            // 
            // chkCombineDist
            // 
            this.chkCombineDist.AutoSize = true;
            this.chkCombineDist.Enabled = false;
            this.chkCombineDist.Location = new System.Drawing.Point(6, 115);
            this.chkCombineDist.Name = "chkCombineDist";
            this.chkCombineDist.Size = new System.Drawing.Size(260, 21);
            this.chkCombineDist.TabIndex = 24;
            this.chkCombineDist.Text = "Combine with distance to selected point";
            this.chkCombineDist.UseVisualStyleBackColor = true;
            this.chkCombineDist.CheckedChanged += new System.EventHandler(this.chkCombineDist_CheckedChanged);
            // 
            // sbarComboMix
            // 
            this.sbarComboMix.LargeChange = 1;
            this.sbarComboMix.Location = new System.Drawing.Point(7, 151);
            this.sbarComboMix.Name = "sbarComboMix";
            this.sbarComboMix.Size = new System.Drawing.Size(341, 17);
            this.sbarComboMix.TabIndex = 22;
            this.sbarComboMix.Visible = false;
            this.sbarComboMix.ValueChanged += new System.EventHandler(this.sbarComboMix_ValueChanged);
            // 
            // lblPctDist2
            // 
            this.lblPctDist2.AutoSize = true;
            this.lblPctDist2.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPctDist2.Location = new System.Drawing.Point(224, 139);
            this.lblPctDist2.Name = "lblPctDist2";
            this.lblPctDist2.Size = new System.Drawing.Size(112, 15);
            this.lblPctDist2.TabIndex = 23;
            this.lblPctDist2.Text = "0% max dist between pts";
            this.lblPctDist2.Visible = false;
            // 
            // lblPctDist1
            // 
            this.lblPctDist1.AutoSize = true;
            this.lblPctDist1.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPctDist1.Location = new System.Drawing.Point(22, 137);
            this.lblPctDist1.Name = "lblPctDist1";
            this.lblPctDist1.Size = new System.Drawing.Size(116, 15);
            this.lblPctDist1.TabIndex = 23;
            this.lblPctDist1.Text = "100% dist to selected point";
            this.lblPctDist1.Visible = false;
            // 
            // rbtnComboDist
            // 
            this.rbtnComboDist.AutoSize = true;
            this.rbtnComboDist.Location = new System.Drawing.Point(300, 101);
            this.rbtnComboDist.Name = "rbtnComboDist";
            this.rbtnComboDist.Size = new System.Drawing.Size(262, 21);
            this.rbtnComboDist.TabIndex = 21;
            this.rbtnComboDist.Text = "combine distance to sel. pt and max dist";
            this.rbtnComboDist.UseVisualStyleBackColor = true;
            this.rbtnComboDist.Visible = false;
            this.rbtnComboDist.CheckedChanged += new System.EventHandler(this.rbtnDist_CheckedChanged);
            // 
            // txtConstDist
            // 
            this.txtConstDist.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConstDist.Location = new System.Drawing.Point(184, 88);
            this.txtConstDist.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtConstDist.Name = "txtConstDist";
            this.txtConstDist.Size = new System.Drawing.Size(69, 20);
            this.txtConstDist.TabIndex = 16;
            this.txtConstDist.Text = "100";
            this.txtConstDist.Visible = false;
            this.txtConstDist.TextChanged += new System.EventHandler(this.txtConstDist_TextChanged);
            // 
            // numMaxIterations
            // 
            this.numMaxIterations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numMaxIterations.Enabled = false;
            this.numMaxIterations.Location = new System.Drawing.Point(246, 358);
            this.numMaxIterations.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numMaxIterations.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numMaxIterations.Name = "numMaxIterations";
            this.numMaxIterations.Size = new System.Drawing.Size(121, 25);
            this.numMaxIterations.TabIndex = 19;
            this.numMaxIterations.ValueChanged += new System.EventHandler(this.numMaxIterations_ValueChanged);
            // 
            // chkMaxIterations
            // 
            this.chkMaxIterations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMaxIterations.AutoSize = true;
            this.chkMaxIterations.Location = new System.Drawing.Point(246, 336);
            this.chkMaxIterations.Name = "chkMaxIterations";
            this.chkMaxIterations.Size = new System.Drawing.Size(110, 21);
            this.chkMaxIterations.TabIndex = 20;
            this.chkMaxIterations.Text = "Max iterations";
            this.chkMaxIterations.UseVisualStyleBackColor = true;
            this.chkMaxIterations.CheckedChanged += new System.EventHandler(this.chkMaxIterations_CheckedChanged);
            // 
            // chkAlongCircle
            // 
            this.chkAlongCircle.AutoSize = true;
            this.chkAlongCircle.Location = new System.Drawing.Point(223, 137);
            this.chkAlongCircle.Name = "chkAlongCircle";
            this.chkAlongCircle.Size = new System.Drawing.Size(101, 21);
            this.chkAlongCircle.TabIndex = 20;
            this.chkAlongCircle.Text = "Circular path";
            this.chkAlongCircle.UseVisualStyleBackColor = true;
            this.chkAlongCircle.Visible = false;
            this.chkAlongCircle.CheckedChanged += new System.EventHandler(this.chkAlongCircle_CheckedChanged);
            // 
            // AlgorithmicSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 437);
            this.Controls.Add(this.groupDistanceMeaning);
            this.Controls.Add(this.chkMaxIterations);
            this.Controls.Add(this.chkAlongCircle);
            this.Controls.Add(this.numMoveN);
            this.Controls.Add(this.numPickN);
            this.Controls.Add(this.numMaxIterations);
            this.Controls.Add(this.numRadius);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtTravelDist);
            this.Controls.Add(this.chkDrawLines);
            this.Controls.Add(this.chkRandomizeBlack);
            this.Controls.Add(this.chkMixColors);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbtnTwoPoints);
            this.Controls.Add(this.rbtnOnePoint);
            this.Controls.Add(this.cboxOnePointMove);
            this.Controls.Add(this.cboxOnePointMethod);
            this.Controls.Add(this.cboxTwoPointMove);
            this.Controls.Add(this.cboxTwoPtMethod);
            this.Controls.Add(this.chkIncludeCurPnt);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AlgorithmicSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Algorithmic settings";
            ((System.ComponentModel.ISupportInitialize)(this.numRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPickN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMoveN)).EndInit();
            this.groupDistanceMeaning.ResumeLayout(false);
            this.groupDistanceMeaning.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxIterations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cboxTwoPtMethod;
        private System.Windows.Forms.RadioButton rbtnOnePoint;
        private System.Windows.Forms.RadioButton rbtnTwoPoints;
        private System.Windows.Forms.ComboBox cboxOnePointMethod;
        private System.Windows.Forms.TextBox txtTravelDist;
        private System.Windows.Forms.CheckBox chkDrawLines;
        private System.Windows.Forms.CheckBox chkRandomizeBlack;
        private System.Windows.Forms.CheckBox chkMixColors;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cboxOnePointMove;
        private System.Windows.Forms.ComboBox cboxTwoPointMove;
        private System.Windows.Forms.NumericUpDown numRadius;
        private System.Windows.Forms.NumericUpDown numPickN;
        private System.Windows.Forms.NumericUpDown numMoveN;
        private System.Windows.Forms.CheckBox chkIncludeCurPnt;
        private System.Windows.Forms.RadioButton rbtnFractionOfDist;
        private System.Windows.Forms.RadioButton rbtnFractionOfMaxDist;
        private System.Windows.Forms.RadioButton rbtnFractionOfMinDist;
        private System.Windows.Forms.RadioButton rbtnConstantDist;
        private System.Windows.Forms.GroupBox groupDistanceMeaning;
        private System.Windows.Forms.HScrollBar sbarComboMix;
        private System.Windows.Forms.Label lblPctDist2;
        private System.Windows.Forms.Label lblPctDist1;
        private System.Windows.Forms.RadioButton rbtnComboDist;
        private System.Windows.Forms.NumericUpDown numMaxIterations;
        private System.Windows.Forms.CheckBox chkMaxIterations;
        private System.Windows.Forms.CheckBox chkCombineDist;
        private System.Windows.Forms.TextBox txtConstDist;
        private System.Windows.Forms.CheckBox chkAlongCircle;
    }
}