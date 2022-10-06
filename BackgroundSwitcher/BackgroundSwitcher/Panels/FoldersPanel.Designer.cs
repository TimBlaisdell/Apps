
namespace BackgroundSwitcher.Panels {
    partial class FoldersPanel {
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
            this.label1 = new System.Windows.Forms.Label();
            this.listFolders = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listNonRecurse = new System.Windows.Forms.ListBox();
            this.txtAddValue = new System.Windows.Forms.TextBox();
            this.btnAddFolder = new System.Windows.Forms.Button();
            this.btnAddNonRecurse = new System.Windows.Forms.Button();
            this.btnAddBaseFolder = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.listBaseFolders = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lblTypeName
            // 
            this.lblTypeName.Size = new System.Drawing.Size(114, 20);
            this.lblTypeName.Text = "MyUserControl";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Folders";
            // 
            // listFolders
            // 
            this.listFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listFolders.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listFolders.FormattingEnabled = true;
            this.listFolders.IntegralHeight = false;
            this.listFolders.ItemHeight = 16;
            this.listFolders.Location = new System.Drawing.Point(8, 27);
            this.listFolders.Name = "listFolders";
            this.listFolders.Size = new System.Drawing.Size(557, 241);
            this.listFolders.Sorted = true;
            this.listFolders.TabIndex = 11;
            this.listFolders.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listFolders_KeyDown);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 271);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Non-recurse folders";
            // 
            // listNonRecurse
            // 
            this.listNonRecurse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listNonRecurse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listNonRecurse.FormattingEnabled = true;
            this.listNonRecurse.IntegralHeight = false;
            this.listNonRecurse.ItemHeight = 16;
            this.listNonRecurse.Location = new System.Drawing.Point(8, 290);
            this.listNonRecurse.Name = "listNonRecurse";
            this.listNonRecurse.Size = new System.Drawing.Size(557, 81);
            this.listNonRecurse.Sorted = true;
            this.listNonRecurse.TabIndex = 11;
            this.listNonRecurse.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listNonRecurse_KeyDown);
            // 
            // txtAddValue
            // 
            this.txtAddValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddValue.Location = new System.Drawing.Point(8, 480);
            this.txtAddValue.Name = "txtAddValue";
            this.txtAddValue.Size = new System.Drawing.Size(347, 22);
            this.txtAddValue.TabIndex = 12;
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFolder.Location = new System.Drawing.Point(361, 480);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Size = new System.Drawing.Size(55, 23);
            this.btnAddFolder.TabIndex = 13;
            this.btnAddFolder.Text = "+ Folder";
            this.btnAddFolder.UseVisualStyleBackColor = true;
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // btnAddNonRecurse
            // 
            this.btnAddNonRecurse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNonRecurse.Location = new System.Drawing.Point(422, 480);
            this.btnAddNonRecurse.Name = "btnAddNonRecurse";
            this.btnAddNonRecurse.Size = new System.Drawing.Size(86, 23);
            this.btnAddNonRecurse.TabIndex = 13;
            this.btnAddNonRecurse.Text = "+ Non-recurse";
            this.btnAddNonRecurse.UseVisualStyleBackColor = true;
            this.btnAddNonRecurse.Click += new System.EventHandler(this.btnAddNonRecurse_Click);
            // 
            // btnAddBaseFolder
            // 
            this.btnAddBaseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddBaseFolder.Location = new System.Drawing.Point(514, 480);
            this.btnAddBaseFolder.Name = "btnAddBaseFolder";
            this.btnAddBaseFolder.Size = new System.Drawing.Size(51, 23);
            this.btnAddBaseFolder.TabIndex = 13;
            this.btnAddBaseFolder.Text = "+ Base";
            this.btnAddBaseFolder.UseVisualStyleBackColor = true;
            this.btnAddBaseFolder.Click += new System.EventHandler(this.btnAddBaseFolder_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 374);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Base folders";
            // 
            // listBaseFolders
            // 
            this.listBaseFolders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBaseFolders.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBaseFolders.FormattingEnabled = true;
            this.listBaseFolders.IntegralHeight = false;
            this.listBaseFolders.ItemHeight = 16;
            this.listBaseFolders.Location = new System.Drawing.Point(8, 393);
            this.listBaseFolders.Name = "listBaseFolders";
            this.listBaseFolders.Size = new System.Drawing.Size(557, 81);
            this.listBaseFolders.Sorted = true;
            this.listBaseFolders.TabIndex = 11;
            this.listBaseFolders.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBaseFolders_KeyDown);
            // 
            // FoldersPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnAddBaseFolder);
            this.Controls.Add(this.btnAddNonRecurse);
            this.Controls.Add(this.btnAddFolder);
            this.Controls.Add(this.txtAddValue);
            this.Controls.Add(this.listBaseFolders);
            this.Controls.Add(this.listNonRecurse);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listFolders);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FoldersPanel";
            this.Size = new System.Drawing.Size(572, 511);
            this.Controls.SetChildIndex(this.lblTypeName, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.listFolders, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.listNonRecurse, 0);
            this.Controls.SetChildIndex(this.listBaseFolders, 0);
            this.Controls.SetChildIndex(this.txtAddValue, 0);
            this.Controls.SetChildIndex(this.btnAddFolder, 0);
            this.Controls.SetChildIndex(this.btnAddNonRecurse, 0);
            this.Controls.SetChildIndex(this.btnAddBaseFolder, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listFolders;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listNonRecurse;
        private System.Windows.Forms.TextBox txtAddValue;
        private System.Windows.Forms.Button btnAddFolder;
        private System.Windows.Forms.Button btnAddNonRecurse;
        private System.Windows.Forms.Button btnAddBaseFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBaseFolders;
    }
}
