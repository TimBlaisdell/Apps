namespace Automata {
   partial class DisplayForm {
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
         this.blockGraphics1 = new BG.BlockGraphics();
         ((System.ComponentModel.ISupportInitialize)(this.blockGraphics1)).BeginInit();
         this.SuspendLayout();
         // 
         // blockGraphics1
         // 
         this.blockGraphics1.Location = new System.Drawing.Point(13, 13);
         this.blockGraphics1.Mode = BG.BlockGraphics.ModeEnum.Zoom;
         this.blockGraphics1.Name = "blockGraphics1";
         this.blockGraphics1.Size = new System.Drawing.Size(579, 587);
         this.blockGraphics1.SuspendRefreshes = false;
         this.blockGraphics1.TabIndex = 0;
         this.blockGraphics1.TabStop = false;
         this.blockGraphics1.ViewArea = new System.Drawing.Rectangle(0, 0, 0, 0);
         // 
         // DisplayForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(604, 612);
         this.Controls.Add(this.blockGraphics1);
         this.Name = "DisplayForm";
         this.Text = "Cellular Automata";
         this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DisplayForm_FormClosed);
         this.Shown += new System.EventHandler(this.DisplayForm_Shown);
         ((System.ComponentModel.ISupportInitialize)(this.blockGraphics1)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private BG.BlockGraphics blockGraphics1;
   }
}

