
namespace BackgroundSwitcher.Panels {
    partial class ImageInfoPanel {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.chkWatchMouse = new System.Windows.Forms.CheckBox();
            this.lblPath = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblOrigSize = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDispSize = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblShowCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblRecentShows = new System.Windows.Forms.Label();
            this.pboxImage = new System.Windows.Forms.PictureBox();
            this.panelUnderPath = new System.Windows.Forms.Panel();
            this.btnOpenImage = new System.Windows.Forms.Button();
            this.btnNeverShow = new System.Windows.Forms.Button();
            this.btnGoToFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pboxImage)).BeginInit();
            this.panelUnderPath.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTypeName
            // 
            this.lblTypeName.Size = new System.Drawing.Size(114, 20);
            this.lblTypeName.Text = "MyUserControl";
            // 
            // chkWatchMouse
            // 
            this.chkWatchMouse.AutoSize = true;
            this.chkWatchMouse.Checked = true;
            this.chkWatchMouse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWatchMouse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWatchMouse.Location = new System.Drawing.Point(8, 8);
            this.chkWatchMouse.Margin = new System.Windows.Forms.Padding(4);
            this.chkWatchMouse.Name = "chkWatchMouse";
            this.chkWatchMouse.Size = new System.Drawing.Size(485, 20);
            this.chkWatchMouse.TabIndex = 9;
            this.chkWatchMouse.Text = "Left-click to open image in editor, right-click to show in explorer, escape to ex" +
    "it.";
            this.chkWatchMouse.UseVisualStyleBackColor = true;
            this.chkWatchMouse.CheckedChanged += new System.EventHandler(this.chkWatchMouse_CheckedChanged);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPath.Location = new System.Drawing.Point(8, 38);
            this.lblPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(107, 18);
            this.lblPath.TabIndex = 7;
            this.lblPath.Text = "C:\\File\\Path.ext";
            this.lblPath.SizeChanged += new System.EventHandler(this.lblPath_SizeChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Original size";
            // 
            // lblOrigSize
            // 
            this.lblOrigSize.AutoSize = true;
            this.lblOrigSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrigSize.Location = new System.Drawing.Point(8, 17);
            this.lblOrigSize.Name = "lblOrigSize";
            this.lblOrigSize.Size = new System.Drawing.Size(25, 16);
            this.lblOrigSize.TabIndex = 8;
            this.lblOrigSize.Text = "0,0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Displayed size";
            // 
            // lblDispSize
            // 
            this.lblDispSize.AutoSize = true;
            this.lblDispSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDispSize.Location = new System.Drawing.Point(8, 54);
            this.lblDispSize.Name = "lblDispSize";
            this.lblDispSize.Size = new System.Drawing.Size(25, 16);
            this.lblDispSize.TabIndex = 8;
            this.lblDispSize.Text = "0,0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Show count";
            // 
            // lblShowCount
            // 
            this.lblShowCount.AutoSize = true;
            this.lblShowCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShowCount.Location = new System.Drawing.Point(8, 91);
            this.lblShowCount.Name = "lblShowCount";
            this.lblShowCount.Size = new System.Drawing.Size(15, 16);
            this.lblShowCount.TabIndex = 8;
            this.lblShowCount.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Recent shows";
            // 
            // lblRecentShows
            // 
            this.lblRecentShows.AutoSize = true;
            this.lblRecentShows.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecentShows.Location = new System.Drawing.Point(8, 130);
            this.lblRecentShows.Name = "lblRecentShows";
            this.lblRecentShows.Size = new System.Drawing.Size(41, 16);
            this.lblRecentShows.TabIndex = 8;
            this.lblRecentShows.Text = "None";
            // 
            // pboxImage
            // 
            this.pboxImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pboxImage.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pboxImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pboxImage.Location = new System.Drawing.Point(160, 0);
            this.pboxImage.Name = "pboxImage";
            this.pboxImage.Size = new System.Drawing.Size(370, 201);
            this.pboxImage.TabIndex = 10;
            this.pboxImage.TabStop = false;
            // 
            // panelUnderPath
            // 
            this.panelUnderPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelUnderPath.Controls.Add(this.btnOpenImage);
            this.panelUnderPath.Controls.Add(this.btnNeverShow);
            this.panelUnderPath.Controls.Add(this.btnGoToFile);
            this.panelUnderPath.Controls.Add(this.lblOrigSize);
            this.panelUnderPath.Controls.Add(this.lblDispSize);
            this.panelUnderPath.Controls.Add(this.label2);
            this.panelUnderPath.Controls.Add(this.label3);
            this.panelUnderPath.Controls.Add(this.label4);
            this.panelUnderPath.Controls.Add(this.label5);
            this.panelUnderPath.Controls.Add(this.pboxImage);
            this.panelUnderPath.Controls.Add(this.lblRecentShows);
            this.panelUnderPath.Controls.Add(this.lblShowCount);
            this.panelUnderPath.Location = new System.Drawing.Point(0, 71);
            this.panelUnderPath.Name = "panelUnderPath";
            this.panelUnderPath.Size = new System.Drawing.Size(538, 240);
            this.panelUnderPath.TabIndex = 15;
            // 
            // btnOpenImage
            // 
            this.btnOpenImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenImage.AutoSize = true;
            this.btnOpenImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenImage.Location = new System.Drawing.Point(427, 206);
            this.btnOpenImage.Margin = new System.Windows.Forms.Padding(2);
            this.btnOpenImage.Name = "btnOpenImage";
            this.btnOpenImage.Size = new System.Drawing.Size(103, 26);
            this.btnOpenImage.TabIndex = 16;
            this.btnOpenImage.Text = "Open image";
            this.btnOpenImage.UseVisualStyleBackColor = true;
            this.btnOpenImage.Click += new System.EventHandler(this.btnOpenImage_Click);
            // 
            // btnNeverShow
            // 
            this.btnNeverShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNeverShow.AutoSize = true;
            this.btnNeverShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNeverShow.Location = new System.Drawing.Point(297, 206);
            this.btnNeverShow.Margin = new System.Windows.Forms.Padding(2);
            this.btnNeverShow.Name = "btnNeverShow";
            this.btnNeverShow.Size = new System.Drawing.Size(126, 26);
            this.btnNeverShow.TabIndex = 17;
            this.btnNeverShow.Text = "Never show again";
            this.btnNeverShow.UseVisualStyleBackColor = true;
            this.btnNeverShow.Click += new System.EventHandler(this.btnNeverShow_Click);
            // 
            // btnGoToFile
            // 
            this.btnGoToFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoToFile.AutoSize = true;
            this.btnGoToFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoToFile.Location = new System.Drawing.Point(177, 206);
            this.btnGoToFile.Margin = new System.Windows.Forms.Padding(2);
            this.btnGoToFile.Name = "btnGoToFile";
            this.btnGoToFile.Size = new System.Drawing.Size(116, 26);
            this.btnGoToFile.TabIndex = 18;
            this.btnGoToFile.Text = "Show in explorer";
            this.btnGoToFile.UseVisualStyleBackColor = true;
            this.btnGoToFile.Click += new System.EventHandler(this.btnGoToFile_Click);
            // 
            // ImageInfoPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panelUnderPath);
            this.Controls.Add(this.chkWatchMouse);
            this.Controls.Add(this.lblPath);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ImageInfoPanel";
            this.Size = new System.Drawing.Size(538, 311);
            this.Controls.SetChildIndex(this.lblPath, 0);
            this.Controls.SetChildIndex(this.chkWatchMouse, 0);
            this.Controls.SetChildIndex(this.panelUnderPath, 0);
            this.Controls.SetChildIndex(this.lblTypeName, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pboxImage)).EndInit();
            this.panelUnderPath.ResumeLayout(false);
            this.panelUnderPath.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkWatchMouse;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOrigSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDispSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblShowCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblRecentShows;
        private System.Windows.Forms.PictureBox pboxImage;
        private System.Windows.Forms.Panel panelUnderPath;
        private System.Windows.Forms.Button btnOpenImage;
        private System.Windows.Forms.Button btnNeverShow;
        private System.Windows.Forms.Button btnGoToFile;
    }
}
