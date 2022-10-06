
namespace BackgroundSwitcher.Panels {
    partial class SettingsPanel {
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
            this.components = new System.ComponentModel.Container();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMinSize = new System.Windows.Forms.TextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtImageExtensions = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numBorderWidth = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numMinShowInterval = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numBorderWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinShowInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTypeName
            // 
            this.lblTypeName.Size = new System.Drawing.Size(114, 20);
            this.lblTypeName.Text = "MyUserControl";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Minimum image size";
            // 
            // txtMinSize
            // 
            this.txtMinSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinSize.Location = new System.Drawing.Point(8, 71);
            this.txtMinSize.Name = "txtMinSize";
            this.txtMinSize.Size = new System.Drawing.Size(130, 22);
            this.txtMinSize.TabIndex = 11;
            this.toolTip.SetToolTip(this.txtMinSize, "Images smaller than this in either height or width will be ignored.");
            this.txtMinSize.TextChanged += new System.EventHandler(this.txtMinSize_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Image extensions";
            this.toolTip.SetToolTip(this.label1, "List the file extensions that should be recognized as images.");
            // 
            // txtImageExtensions
            // 
            this.txtImageExtensions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImageExtensions.Location = new System.Drawing.Point(8, 27);
            this.txtImageExtensions.Name = "txtImageExtensions";
            this.txtImageExtensions.Size = new System.Drawing.Size(209, 22);
            this.txtImageExtensions.TabIndex = 11;
            this.toolTip.SetToolTip(this.txtImageExtensions, "A list of file extensions that will be used as images.");
            this.txtImageExtensions.TextChanged += new System.EventHandler(this.txtImageExtensions_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Border width";
            // 
            // numBorderWidth
            // 
            this.numBorderWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numBorderWidth.Location = new System.Drawing.Point(8, 115);
            this.numBorderWidth.Name = "numBorderWidth";
            this.numBorderWidth.Size = new System.Drawing.Size(82, 22);
            this.numBorderWidth.TabIndex = 12;
            this.toolTip.SetToolTip(this.numBorderWidth, "This is the width of the border between images.");
            this.numBorderWidth.ValueChanged += new System.EventHandler(this.numBorderWidth_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Minimum show interval (days)";
            // 
            // numMinShowInterval
            // 
            this.numMinShowInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numMinShowInterval.Location = new System.Drawing.Point(8, 159);
            this.numMinShowInterval.Name = "numMinShowInterval";
            this.numMinShowInterval.Size = new System.Drawing.Size(82, 22);
            this.numMinShowInterval.TabIndex = 12;
            this.toolTip.SetToolTip(this.numMinShowInterval, "The same image will never be shown twice within this interval of days.");
            this.numMinShowInterval.ValueChanged += new System.EventHandler(this.numMinShowInterval_ValueChanged);
            // 
            // SettingsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.numMinShowInterval);
            this.Controls.Add(this.numBorderWidth);
            this.Controls.Add(this.txtImageExtensions);
            this.Controls.Add(this.txtMinSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "SettingsPanel";
            this.Size = new System.Drawing.Size(620, 536);
            this.Controls.SetChildIndex(this.lblTypeName, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtMinSize, 0);
            this.Controls.SetChildIndex(this.txtImageExtensions, 0);
            this.Controls.SetChildIndex(this.numBorderWidth, 0);
            this.Controls.SetChildIndex(this.numMinShowInterval, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numBorderWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinShowInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMinSize;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtImageExtensions;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numBorderWidth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numMinShowInterval;
    }
}
