namespace Automata {
   partial class ControlForm {
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
         this.tabConway = new System.Windows.Forms.TabPage();
         this.tabWolfram = new System.Windows.Forms.TabPage();
         this.btnLoad = new System.Windows.Forms.Button();
         this.tabControl.SuspendLayout();
         this.tabConway.SuspendLayout();
         this.SuspendLayout();
         // 
         // tabControl
         // 
         this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.tabControl.Controls.Add(this.tabConway);
         this.tabControl.Controls.Add(this.tabWolfram);
         this.tabControl.Location = new System.Drawing.Point(12, 12);
         this.tabControl.Name = "tabControl";
         this.tabControl.SelectedIndex = 0;
         this.tabControl.Size = new System.Drawing.Size(394, 262);
         this.tabControl.TabIndex = 0;
         // 
         // tabConway
         // 
         this.tabConway.Controls.Add(this.btnLoad);
         this.tabConway.Location = new System.Drawing.Point(4, 22);
         this.tabConway.Name = "tabConway";
         this.tabConway.Padding = new System.Windows.Forms.Padding(3);
         this.tabConway.Size = new System.Drawing.Size(386, 236);
         this.tabConway.TabIndex = 0;
         this.tabConway.Text = "Conway";
         this.tabConway.UseVisualStyleBackColor = true;
         // 
         // tabWolfram
         // 
         this.tabWolfram.Location = new System.Drawing.Point(4, 22);
         this.tabWolfram.Name = "tabWolfram";
         this.tabWolfram.Padding = new System.Windows.Forms.Padding(3);
         this.tabWolfram.Size = new System.Drawing.Size(519, 526);
         this.tabWolfram.TabIndex = 1;
         this.tabWolfram.Text = "Wolfram";
         this.tabWolfram.UseVisualStyleBackColor = true;
         // 
         // btnLoad
         // 
         this.btnLoad.Location = new System.Drawing.Point(7, 7);
         this.btnLoad.Name = "btnLoad";
         this.btnLoad.Size = new System.Drawing.Size(75, 23);
         this.btnLoad.TabIndex = 0;
         this.btnLoad.Text = "Load file";
         this.btnLoad.UseVisualStyleBackColor = true;
         // 
         // ControlForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(418, 286);
         this.Controls.Add(this.tabControl);
         this.Name = "ControlForm";
         this.Text = "ControlForm";
         this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ControlForm_FormClosed);
         this.tabControl.ResumeLayout(false);
         this.tabConway.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.TabControl tabControl;
      private System.Windows.Forms.TabPage tabConway;
      private System.Windows.Forms.TabPage tabWolfram;
      private System.Windows.Forms.Button btnLoad;

   }
}