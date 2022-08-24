
namespace BackgroundSwitcher {
    partial class FocusRectEditor {
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
            this.btnSet = new System.Windows.Forms.Button();
            this.btnSkip = new System.Windows.Forms.Button();
            this.lblCoords = new System.Windows.Forms.Label();
            this.lblImagePath = new System.Windows.Forms.Label();
            this.lblImageSize = new System.Windows.Forms.Label();
            this.btnUseWholeImage = new System.Windows.Forms.Button();
            this.numHeight = new System.Windows.Forms.NumericUpDown();
            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.menuImage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuEditThisImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuGoToFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStayInFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSelectFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuAutoRect = new System.Windows.Forms.ToolStripMenuItem();
            this.panelCanvas = new BackgroundSwitcher.ImagePanel();
            this.lblRemaining = new System.Windows.Forms.Label();
            this.btnNeverShow = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.menuReloadImage = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            this.menuImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSet
            // 
            this.btnSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSet.Image = global::BackgroundSwitcher.Properties.Resources.S;
            this.btnSet.Location = new System.Drawing.Point(971, 592);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(126, 26);
            this.btnSet.TabIndex = 1;
            this.btnSet.Text = "Set && next";
            this.btnSet.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnSkip
            // 
            this.btnSkip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSkip.Image = global::BackgroundSwitcher.Properties.Resources.X;
            this.btnSkip.Location = new System.Drawing.Point(707, 592);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(126, 26);
            this.btnSkip.TabIndex = 1;
            this.btnSkip.Text = "Skip this one";
            this.btnSkip.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnSkip.UseVisualStyleBackColor = true;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // lblCoords
            // 
            this.lblCoords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCoords.AutoSize = true;
            this.lblCoords.Location = new System.Drawing.Point(12, 597);
            this.lblCoords.Name = "lblCoords";
            this.lblCoords.Size = new System.Drawing.Size(25, 13);
            this.lblCoords.TabIndex = 2;
            this.lblCoords.Text = "0, 0";
            // 
            // lblImagePath
            // 
            this.lblImagePath.AutoSize = true;
            this.lblImagePath.Location = new System.Drawing.Point(12, 9);
            this.lblImagePath.Name = "lblImagePath";
            this.lblImagePath.Size = new System.Drawing.Size(28, 13);
            this.lblImagePath.TabIndex = 2;
            this.lblImagePath.Text = "path";
            // 
            // lblImageSize
            // 
            this.lblImageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblImageSize.AutoSize = true;
            this.lblImageSize.Location = new System.Drawing.Point(1072, 9);
            this.lblImageSize.Name = "lblImageSize";
            this.lblImageSize.Size = new System.Drawing.Size(25, 13);
            this.lblImageSize.TabIndex = 2;
            this.lblImageSize.Text = "0, 0";
            // 
            // btnUseWholeImage
            // 
            this.btnUseWholeImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUseWholeImage.Image = global::BackgroundSwitcher.Properties.Resources.F;
            this.btnUseWholeImage.Location = new System.Drawing.Point(839, 592);
            this.btnUseWholeImage.Name = "btnUseWholeImage";
            this.btnUseWholeImage.Size = new System.Drawing.Size(136, 26);
            this.btnUseWholeImage.TabIndex = 1;
            this.btnUseWholeImage.Text = "Set to full image";
            this.btnUseWholeImage.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnUseWholeImage.UseVisualStyleBackColor = true;
            this.btnUseWholeImage.Click += new System.EventHandler(this.btnUseWholeImage_Click);
            // 
            // numHeight
            // 
            this.numHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numHeight.Location = new System.Drawing.Point(505, 595);
            this.numHeight.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numHeight.Name = "numHeight";
            this.numHeight.Size = new System.Drawing.Size(64, 20);
            this.numHeight.TabIndex = 3;
            this.numHeight.Value = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numHeight.Visible = false;
            this.numHeight.ValueChanged += new System.EventHandler(this.num_ValueChanged);
            // 
            // numWidth
            // 
            this.numWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numWidth.Location = new System.Drawing.Point(430, 595);
            this.numWidth.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numWidth.Name = "numWidth";
            this.numWidth.Size = new System.Drawing.Size(64, 20);
            this.numWidth.TabIndex = 3;
            this.numWidth.Value = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numWidth.Visible = false;
            this.numWidth.ValueChanged += new System.EventHandler(this.num_ValueChanged);
            // 
            // numY
            // 
            this.numY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numY.Location = new System.Drawing.Point(355, 595);
            this.numY.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numY.Name = "numY";
            this.numY.Size = new System.Drawing.Size(64, 20);
            this.numY.TabIndex = 3;
            this.numY.Value = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numY.Visible = false;
            this.numY.ValueChanged += new System.EventHandler(this.num_ValueChanged);
            // 
            // numX
            // 
            this.numX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numX.Location = new System.Drawing.Point(280, 595);
            this.numX.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numX.Name = "numX";
            this.numX.Size = new System.Drawing.Size(64, 20);
            this.numX.TabIndex = 3;
            this.numX.Value = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numX.Visible = false;
            this.numX.ValueChanged += new System.EventHandler(this.num_ValueChanged);
            // 
            // menuImage
            // 
            this.menuImage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEditThisImage,
            this.menuReloadImage,
            this.toolStripSeparator1,
            this.menuGoToFolder,
            this.menuStayInFolder,
            this.menuSelectFolder,
            this.toolStripSeparator2,
            this.menuAutoRect});
            this.menuImage.Name = "menuImage";
            this.menuImage.Size = new System.Drawing.Size(245, 170);
            // 
            // menuEditThisImage
            // 
            this.menuEditThisImage.Image = global::BackgroundSwitcher.Properties.Resources.E;
            this.menuEditThisImage.Name = "menuEditThisImage";
            this.menuEditThisImage.ShortcutKeyDisplayString = "";
            this.menuEditThisImage.Size = new System.Drawing.Size(244, 22);
            this.menuEditThisImage.Text = "Edit this image";
            this.menuEditThisImage.Click += new System.EventHandler(this.menuEditThisImage_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(241, 6);
            // 
            // menuGoToFolder
            // 
            this.menuGoToFolder.Name = "menuGoToFolder";
            this.menuGoToFolder.Size = new System.Drawing.Size(244, 22);
            this.menuGoToFolder.Text = "Go to folder containing image";
            this.menuGoToFolder.Click += new System.EventHandler(this.menuGoToFolder_Click);
            // 
            // menuStayInFolder
            // 
            this.menuStayInFolder.Name = "menuStayInFolder";
            this.menuStayInFolder.Size = new System.Drawing.Size(244, 22);
            this.menuStayInFolder.Text = "Stay in folder containing this file";
            this.menuStayInFolder.Click += new System.EventHandler(this.menuStayInFolder_Click);
            // 
            // menuSelectFolder
            // 
            this.menuSelectFolder.Name = "menuSelectFolder";
            this.menuSelectFolder.Size = new System.Drawing.Size(244, 22);
            this.menuSelectFolder.Text = "Stay in folder...";
            this.menuSelectFolder.Click += new System.EventHandler(this.menuSelectFolder_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(241, 6);
            // 
            // menuAutoRect
            // 
            this.menuAutoRect.Image = global::BackgroundSwitcher.Properties.Resources.A;
            this.menuAutoRect.Name = "menuAutoRect";
            this.menuAutoRect.ShortcutKeyDisplayString = "";
            this.menuAutoRect.Size = new System.Drawing.Size(244, 22);
            this.menuAutoRect.Text = "Create rect around image";
            this.menuAutoRect.Click += new System.EventHandler(this.menuAutoRect_Click);
            // 
            // panelCanvas
            // 
            this.panelCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCanvas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCanvas.ContextMenuStrip = this.menuImage;
            this.panelCanvas.Location = new System.Drawing.Point(12, 25);
            this.panelCanvas.Name = "panelCanvas";
            this.panelCanvas.SelectedRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.panelCanvas.Size = new System.Drawing.Size(1085, 561);
            this.panelCanvas.TabIndex = 0;
            this.panelCanvas.SelectedRectChanged += new System.EventHandler(this.panelCanvas_SelectedRectChanged);
            this.panelCanvas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.panelCanvas_KeyDown);
            this.panelCanvas.MouseLeave += new System.EventHandler(this.panelCanvas_MouseLeave);
            this.panelCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelCanvas_MouseMove);
            // 
            // lblRemaining
            // 
            this.lblRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRemaining.Location = new System.Drawing.Point(162, 597);
            this.lblRemaining.Name = "lblRemaining";
            this.lblRemaining.Size = new System.Drawing.Size(112, 20);
            this.lblRemaining.TabIndex = 2;
            this.lblRemaining.Text = "Remaining: 00000";
            this.lblRemaining.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnNeverShow
            // 
            this.btnNeverShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNeverShow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnNeverShow.Image = global::BackgroundSwitcher.Properties.Resources.N;
            this.btnNeverShow.Location = new System.Drawing.Point(575, 592);
            this.btnNeverShow.Name = "btnNeverShow";
            this.btnNeverShow.Size = new System.Drawing.Size(126, 26);
            this.btnNeverShow.TabIndex = 1;
            this.btnNeverShow.Text = "Never show";
            this.btnNeverShow.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnNeverShow.UseVisualStyleBackColor = true;
            this.btnNeverShow.Click += new System.EventHandler(this.btnNeverShow_Click);
            // 
            // menuReloadImage
            // 
            this.menuReloadImage.Name = "menuReloadImage";
            this.menuReloadImage.Size = new System.Drawing.Size(244, 22);
            this.menuReloadImage.Text = "Reload this image";
            this.menuReloadImage.Click += new System.EventHandler(this.menuReloadImage_Click);
            // 
            // FocusRectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 627);
            this.Controls.Add(this.numX);
            this.Controls.Add(this.numY);
            this.Controls.Add(this.numWidth);
            this.Controls.Add(this.numHeight);
            this.Controls.Add(this.panelCanvas);
            this.Controls.Add(this.lblImagePath);
            this.Controls.Add(this.lblImageSize);
            this.Controls.Add(this.lblRemaining);
            this.Controls.Add(this.lblCoords);
            this.Controls.Add(this.btnUseWholeImage);
            this.Controls.Add(this.btnNeverShow);
            this.Controls.Add(this.btnSkip);
            this.Controls.Add(this.btnSet);
            this.Name = "FocusRectEditor";
            this.Text = "FocusRectEditor";
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            this.menuImage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.Label lblCoords;
        private ImagePanel panelCanvas;
        private System.Windows.Forms.Label lblImagePath;
        private System.Windows.Forms.Label lblImageSize;
        private System.Windows.Forms.Button btnUseWholeImage;
        private System.Windows.Forms.NumericUpDown numHeight;
        private System.Windows.Forms.NumericUpDown numWidth;
        private System.Windows.Forms.NumericUpDown numY;
        private System.Windows.Forms.NumericUpDown numX;
        private System.Windows.Forms.ContextMenuStrip menuImage;
        private System.Windows.Forms.ToolStripMenuItem menuEditThisImage;
        private System.Windows.Forms.ToolStripMenuItem menuGoToFolder;
        private System.Windows.Forms.ToolStripMenuItem menuStayInFolder;
        private System.Windows.Forms.ToolStripMenuItem menuAutoRect;
        private System.Windows.Forms.Label lblRemaining;
        private System.Windows.Forms.Button btnNeverShow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuSelectFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ToolStripMenuItem menuReloadImage;
    }
}