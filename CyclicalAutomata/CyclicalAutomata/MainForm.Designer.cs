namespace CyclicalAutomata {
   partial class MainForm {
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
         this.timer = new System.Windows.Forms.Timer(this.components);
         this.bwWorker = new System.ComponentModel.BackgroundWorker();
         this.SuspendLayout();
         // 
         // timer
         // 
         this.timer.Enabled = true;
         this.timer.Interval = 50;
         this.timer.Tick += new System.EventHandler(this.timer_Tick);
         // 
         // bwWorker
         // 
         this.bwWorker.WorkerSupportsCancellation = true;
         this.bwWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwWorker_DoWork);
         // 
         // MainForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(300, 300);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
         this.Name = "MainForm";
         this.Text = "MainForm";
         this.Activated += new System.EventHandler(this.MainForm_Activated);
         this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
         this.Shown += new System.EventHandler(this.Form1_Shown);
         this.LocationChanged += new System.EventHandler(this.MainForm_LocationChanged);
         this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
         this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
         this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
         this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
         this.ResumeLayout(false);

      }

      #endregion

      private System.ComponentModel.BackgroundWorker bwWorker;
      public System.Windows.Forms.Timer timer;
   }
}

