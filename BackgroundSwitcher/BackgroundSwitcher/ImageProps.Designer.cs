
namespace BackgroundSwitcher {
    sealed partial class ImageProps {
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
            this.lblValues = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGoToFile = new System.Windows.Forms.Button();
            this.btnNeverShow = new System.Windows.Forms.Button();
            this.lblMouseCoords = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnOpenImage = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFocusRectEdit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblValues
            // 
            this.lblValues.AutoSize = true;
            this.lblValues.Location = new System.Drawing.Point(86, 33);
            this.lblValues.Name = "lblValues";
            this.lblValues.Size = new System.Drawing.Size(82, 13);
            this.lblValues.TabIndex = 0;
            this.lblValues.Text = "C:\\File\\Path.ext";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 65);
            this.label1.TabIndex = 1;
            this.label1.Text = "Path:\r\nOriginal size:\r\nDisplayed size:\r\nShow count:\r\nRecent shows:";
            // 
            // btnGoToFile
            // 
            this.btnGoToFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGoToFile.AutoSize = true;
            this.btnGoToFile.Location = new System.Drawing.Point(8, 149);
            this.btnGoToFile.Margin = new System.Windows.Forms.Padding(2);
            this.btnGoToFile.Name = "btnGoToFile";
            this.btnGoToFile.Size = new System.Drawing.Size(95, 23);
            this.btnGoToFile.TabIndex = 2;
            this.btnGoToFile.Text = "Show in explorer";
            this.btnGoToFile.UseVisualStyleBackColor = true;
            this.btnGoToFile.Click += new System.EventHandler(this.btnGoToFile_Click);
            // 
            // btnNeverShow
            // 
            this.btnNeverShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNeverShow.AutoSize = true;
            this.btnNeverShow.Location = new System.Drawing.Point(107, 149);
            this.btnNeverShow.Margin = new System.Windows.Forms.Padding(2);
            this.btnNeverShow.Name = "btnNeverShow";
            this.btnNeverShow.Size = new System.Drawing.Size(103, 23);
            this.btnNeverShow.TabIndex = 2;
            this.btnNeverShow.Text = "Never show again";
            this.btnNeverShow.UseVisualStyleBackColor = true;
            this.btnNeverShow.Click += new System.EventHandler(this.btnNeverShow_Click);
            // 
            // lblMouseCoords
            // 
            this.lblMouseCoords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMouseCoords.AutoSize = true;
            this.lblMouseCoords.Location = new System.Drawing.Point(539, 159);
            this.lblMouseCoords.Name = "lblMouseCoords";
            this.lblMouseCoords.Size = new System.Drawing.Size(28, 13);
            this.lblMouseCoords.TabIndex = 3;
            this.lblMouseCoords.Text = "(0,0)";
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(8, 131);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(65, 16);
            this.lblMessage.TabIndex = 4;
            this.lblMessage.Text = "message";
            this.lblMessage.Visible = false;
            // 
            // btnOpenImage
            // 
            this.btnOpenImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenImage.AutoSize = true;
            this.btnOpenImage.Location = new System.Drawing.Point(214, 149);
            this.btnOpenImage.Margin = new System.Windows.Forms.Padding(2);
            this.btnOpenImage.Name = "btnOpenImage";
            this.btnOpenImage.Size = new System.Drawing.Size(103, 23);
            this.btnOpenImage.TabIndex = 2;
            this.btnOpenImage.Text = "Open image";
            this.btnOpenImage.UseVisualStyleBackColor = true;
            this.btnOpenImage.Click += new System.EventHandler(this.btnOpenImage_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(466, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Left-click to open image in editor, right-click to show in explorer, escape to ex" +
    "it.";
            // 
            // btnFocusRectEdit
            // 
            this.btnFocusRectEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFocusRectEdit.AutoSize = true;
            this.btnFocusRectEdit.Location = new System.Drawing.Point(321, 149);
            this.btnFocusRectEdit.Margin = new System.Windows.Forms.Padding(2);
            this.btnFocusRectEdit.Name = "btnFocusRectEdit";
            this.btnFocusRectEdit.Size = new System.Drawing.Size(136, 23);
            this.btnFocusRectEdit.TabIndex = 2;
            this.btnFocusRectEdit.Text = "Open FocusRect editor";
            this.btnFocusRectEdit.UseVisualStyleBackColor = true;
            this.btnFocusRectEdit.Click += new System.EventHandler(this.btnFocusRectEdit_Click);
            // 
            // ImageProps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 181);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblMouseCoords);
            this.Controls.Add(this.lblValues);
            this.Controls.Add(this.btnFocusRectEdit);
            this.Controls.Add(this.btnOpenImage);
            this.Controls.Add(this.btnNeverShow);
            this.Controls.Add(this.btnGoToFile);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ImageProps";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ImageProps";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblValues;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGoToFile;
        private System.Windows.Forms.Button btnNeverShow;
        private System.Windows.Forms.Label lblMouseCoords;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnOpenImage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFocusRectEdit;
    }
}