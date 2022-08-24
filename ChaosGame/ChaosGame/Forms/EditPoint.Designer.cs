
namespace ChaosGame.Forms {
    partial class EditPoint {
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
            this.numX = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numY = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btnColor = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).BeginInit();
            this.SuspendLayout();
            // 
            // numX
            // 
            this.numX.Location = new System.Drawing.Point(12, 30);
            this.numX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numX.Name = "numX";
            this.numX.Size = new System.Drawing.Size(88, 25);
            this.numX.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "X";
            // 
            // numY
            // 
            this.numY.Location = new System.Drawing.Point(106, 30);
            this.numY.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numY.Name = "numY";
            this.numY.Size = new System.Drawing.Size(88, 25);
            this.numY.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y";
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(12, 62);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(179, 25);
            this.btnColor.TabIndex = 2;
            this.btnColor.Text = "button1";
            this.btnColor.UseVisualStyleBackColor = true;
            // 
            // EditPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 100);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numY);
            this.Controls.Add(this.numX);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "EditPoint";
            this.Text = "Edit point";
            ((System.ComponentModel.ISupportInitialize)(this.numX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btnColor;
    }
}