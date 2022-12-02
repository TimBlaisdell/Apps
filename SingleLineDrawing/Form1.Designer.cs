namespace SingleLineDrawing
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnStart = new System.Windows.Forms.Button();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuSaveImage = new System.Windows.Forms.ToolStripMenuItem();
            this.btnTargetImage = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.numMagnitude = new System.Windows.Forms.NumericUpDown();
            this.lblMagnitude = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.chkRotatingSequence = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.numIncrement = new System.Windows.Forms.NumericUpDown();
            this.lineDrawer = new SingleLineDrawing.LineDrawer();
            this.contextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMagnitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIncrement)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(502, 583);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSaveImage});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(144, 26);
            // 
            // menuSaveImage
            // 
            this.menuSaveImage.Name = "menuSaveImage";
            this.menuSaveImage.Size = new System.Drawing.Size(143, 22);
            this.menuSaveImage.Text = "Save image...";
            this.menuSaveImage.Click += new System.EventHandler(this.menuSaveImage_Click);
            // 
            // btnTargetImage
            // 
            this.btnTargetImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTargetImage.Location = new System.Drawing.Point(12, 583);
            this.btnTargetImage.Name = "btnTargetImage";
            this.btnTargetImage.Size = new System.Drawing.Size(115, 23);
            this.btnTargetImage.TabIndex = 2;
            this.btnTargetImage.Text = "Load target image";
            this.btnTargetImage.UseVisualStyleBackColor = true;
            this.btnTargetImage.Click += new System.EventHandler(this.btnTargetImage_Click);
            // 
            // numMagnitude
            // 
            this.numMagnitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numMagnitude.Location = new System.Drawing.Point(441, 586);
            this.numMagnitude.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMagnitude.Name = "numMagnitude";
            this.numMagnitude.Size = new System.Drawing.Size(55, 20);
            this.numMagnitude.TabIndex = 3;
            this.numMagnitude.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numMagnitude.ValueChanged += new System.EventHandler(this.numMagnitude_ValueChanged);
            // 
            // lblMagnitude
            // 
            this.lblMagnitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMagnitude.AutoSize = true;
            this.lblMagnitude.Location = new System.Drawing.Point(334, 588);
            this.lblMagnitude.Name = "lblMagnitude";
            this.lblMagnitude.Size = new System.Drawing.Size(101, 13);
            this.lblMagnitude.TabIndex = 4;
            this.lblMagnitude.Text = "Spiralling magnitude";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "png";
            // 
            // chkRotatingSequence
            // 
            this.chkRotatingSequence.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkRotatingSequence.AutoSize = true;
            this.chkRotatingSequence.Location = new System.Drawing.Point(133, 587);
            this.chkRotatingSequence.Name = "chkRotatingSequence";
            this.chkRotatingSequence.Size = new System.Drawing.Size(145, 17);
            this.chkRotatingSequence.TabIndex = 5;
            this.chkRotatingSequence.Text = "Create rotating sequence";
            this.chkRotatingSequence.UseVisualStyleBackColor = true;
            this.chkRotatingSequence.CheckedChanged += new System.EventHandler(this.chkRotatingSequence_CheckedChanged);
            // 
            // numIncrement
            // 
            this.numIncrement.Location = new System.Drawing.Point(285, 587);
            this.numIncrement.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numIncrement.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numIncrement.Name = "numIncrement";
            this.numIncrement.Size = new System.Drawing.Size(43, 20);
            this.numIncrement.TabIndex = 6;
            this.numIncrement.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numIncrement.Visible = false;
            this.numIncrement.ValueChanged += new System.EventHandler(this.numIncrement_ValueChanged);
            // 
            // lineDrawer
            // 
            this.lineDrawer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineDrawer.ContextMenuStrip = this.contextMenu;
            this.lineDrawer.CreateRotatingSequence = false;
            this.lineDrawer.Location = new System.Drawing.Point(12, 12);
            this.lineDrawer.Name = "lineDrawer";
            this.lineDrawer.RotatingSequenceAngleIncrement = 0;
            this.lineDrawer.RotatingSequenceFolder = null;
            this.lineDrawer.Size = new System.Drawing.Size(565, 565);
            this.lineDrawer.SpiralDistanceMagnitude = 0;
            this.lineDrawer.TabIndex = 0;
            this.lineDrawer.TargetImage = null;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 618);
            this.Controls.Add(this.numIncrement);
            this.Controls.Add(this.chkRotatingSequence);
            this.Controls.Add(this.lblMagnitude);
            this.Controls.Add(this.numMagnitude);
            this.Controls.Add(this.btnTargetImage);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lineDrawer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.contextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numMagnitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIncrement)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LineDrawer lineDrawer;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnTargetImage;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.NumericUpDown numMagnitude;
        private System.Windows.Forms.Label lblMagnitude;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem menuSaveImage;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.CheckBox chkRotatingSequence;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.NumericUpDown numIncrement;
    }
}

