
namespace BackgroundSwitcher {
    sealed partial class MainForm {
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblMessage = new System.Windows.Forms.Label();
            this.multiSliderPanel = new MSP.MultiSliderPanel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.pageImageInfo = new System.Windows.Forms.TabPage();
            this.pageFolders = new System.Windows.Forms.TabPage();
            this.pageSettings = new System.Windows.Forms.TabPage();
            this.pageFocusRects = new System.Windows.Forms.TabPage();
            this.panelFolders = new BackgroundSwitcher.Panels.FoldersPanel();
            this.panelSettings = new BackgroundSwitcher.Panels.SettingsPanel();
            this.panelImageInfo = new BackgroundSwitcher.Panels.ImageInfoPanel();
            this.panelFocusRects = new BackgroundSwitcher.Panels.FocusRectsPanel();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
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
            this.lblMessage.Location = new System.Drawing.Point(8, 416);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(65, 16);
            this.lblMessage.TabIndex = 4;
            this.lblMessage.Text = "message";
            this.lblMessage.Visible = false;
            // 
            // multiSliderPanel
            // 
            this.multiSliderPanel.Accelleration = 1;
            this.multiSliderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.multiSliderPanel.AnimationSleepMS = 10;
            this.multiSliderPanel.BorderWidth = 0;
            this.multiSliderPanel.DelaySlideMain = false;
            this.multiSliderPanel.InitialSpeed = 1;
            this.multiSliderPanel.Location = new System.Drawing.Point(0, 35);
            this.multiSliderPanel.Main = null;
            this.multiSliderPanel.Name = "multiSliderPanel";
            this.multiSliderPanel.Size = new System.Drawing.Size(617, 397);
            this.multiSliderPanel.StopAnimation = false;
            this.multiSliderPanel.TabIndex = 7;
            this.multiSliderPanel.ZOrderCorrection = false;
            this.multiSliderPanel.SizeChanged += new System.EventHandler(this.multiSliderPanel_SizeChanged);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.pageImageInfo);
            this.tabControl.Controls.Add(this.pageFolders);
            this.tabControl.Controls.Add(this.pageSettings);
            this.tabControl.Controls.Add(this.pageFocusRects);
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(1, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(605, 29);
            this.tabControl.TabIndex = 9;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            this.tabControl.TabIndexChanged += new System.EventHandler(this.tabControl_TabIndexChanged);
            // 
            // pageImageInfo
            // 
            this.pageImageInfo.Location = new System.Drawing.Point(4, 25);
            this.pageImageInfo.Name = "pageImageInfo";
            this.pageImageInfo.Padding = new System.Windows.Forms.Padding(3);
            this.pageImageInfo.Size = new System.Drawing.Size(597, 0);
            this.pageImageInfo.TabIndex = 0;
            this.pageImageInfo.Text = "Image info";
            this.pageImageInfo.UseVisualStyleBackColor = true;
            // 
            // pageFolders
            // 
            this.pageFolders.Location = new System.Drawing.Point(4, 25);
            this.pageFolders.Name = "pageFolders";
            this.pageFolders.Padding = new System.Windows.Forms.Padding(3);
            this.pageFolders.Size = new System.Drawing.Size(597, 0);
            this.pageFolders.TabIndex = 2;
            this.pageFolders.Text = "Folders";
            this.pageFolders.UseVisualStyleBackColor = true;
            // 
            // pageSettings
            // 
            this.pageSettings.Location = new System.Drawing.Point(4, 25);
            this.pageSettings.Name = "pageSettings";
            this.pageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.pageSettings.Size = new System.Drawing.Size(597, 0);
            this.pageSettings.TabIndex = 1;
            this.pageSettings.Text = "Settings";
            this.pageSettings.UseVisualStyleBackColor = true;
            // 
            // pageFocusRects
            // 
            this.pageFocusRects.Location = new System.Drawing.Point(4, 25);
            this.pageFocusRects.Name = "pageFocusRects";
            this.pageFocusRects.Size = new System.Drawing.Size(597, 0);
            this.pageFocusRects.TabIndex = 3;
            this.pageFocusRects.Text = "Focus regions";
            this.pageFocusRects.UseVisualStyleBackColor = true;
            // 
            // panelFolders
            // 
            this.panelFolders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFolders.Location = new System.Drawing.Point(294, -4);
            this.panelFolders.Name = "panelFolders";
            this.panelFolders.Size = new System.Drawing.Size(101, 22);
            this.panelFolders.TabIndex = 11;
            this.panelFolders.TargetSize = new System.Drawing.Size(0, 0);
            this.panelFolders.ShowMessage += new System.EventHandler<BackgroundSwitcher.Panels.MessageInfo>(this.panel_ShowMessage);
            // 
            // panelSettings
            // 
            this.panelSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSettings.Location = new System.Drawing.Point(394, -4);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(107, 24);
            this.panelSettings.TabIndex = 10;
            this.panelSettings.TargetSize = new System.Drawing.Size(0, 0);
            this.panelSettings.WatchMouseChanged += new System.EventHandler(this.chkWatchMouseChanged);
            this.panelSettings.ShowMessage += new System.EventHandler<BackgroundSwitcher.Panels.MessageInfo>(this.panel_ShowMessage);
            // 
            // panelImageInfo
            // 
            this.panelImageInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelImageInfo.Location = new System.Drawing.Point(500, -3);
            this.panelImageInfo.Margin = new System.Windows.Forms.Padding(4);
            this.panelImageInfo.Name = "panelImageInfo";
            this.panelImageInfo.Size = new System.Drawing.Size(123, 21);
            this.panelImageInfo.TabIndex = 8;
            this.panelImageInfo.TargetSize = new System.Drawing.Size(0, 0);
            this.panelImageInfo.WatchMouse = true;
            this.panelImageInfo.GoToFile += new System.EventHandler(this.btnGoToFile_Click);
            this.panelImageInfo.NeverShow += new System.EventHandler(this.btnNeverShow_Click);
            this.panelImageInfo.OpenFocusRectEditor += new System.EventHandler(this.btnFocusRectEdit_Click);
            this.panelImageInfo.OpenImage += new System.EventHandler(this.btnOpenImage_Click);
            this.panelImageInfo.WatchMouseChanged += new System.EventHandler(this.chkWatchMouseChanged);
            this.panelImageInfo.ShowMessage += new System.EventHandler<BackgroundSwitcher.Panels.MessageInfo>(this.panel_ShowMessage);
            // 
            // panelFocusRects
            // 
            this.panelFocusRects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFocusRects.Location = new System.Drawing.Point(157, -3);
            this.panelFocusRects.Name = "panelFocusRects";
            this.panelFocusRects.Size = new System.Drawing.Size(137, 22);
            this.panelFocusRects.TabIndex = 12;
            this.panelFocusRects.TargetSize = new System.Drawing.Size(0, 0);
            this.panelFocusRects.EditImage += new System.EventHandler<string>(this.panelFocusRects_EditImage);
            this.panelFocusRects.PrepFocusRectsPanel += new System.EventHandler<BackgroundSwitcher.Panels.FocusRectsPanel.PrepFocusRectsPanelEventArgs>(this.panelFocusRects_PrepFocusRectsPanel);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(619, 434);
            this.Controls.Add(this.panelFocusRects);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.panelFolders);
            this.Controls.Add(this.panelSettings);
            this.Controls.Add(this.panelImageInfo);
            this.Controls.Add(this.multiSliderPanel);
            this.Controls.Add(this.tabControl);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MainForm";
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblMessage;
        private MSP.MultiSliderPanel multiSliderPanel;
        private Panels.ImageInfoPanel panelImageInfo;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage pageImageInfo;
        private System.Windows.Forms.TabPage pageSettings;
        private Panels.FoldersPanel panelFolders;
        private System.Windows.Forms.TabPage pageFolders;
        private Panels.SettingsPanel panelSettings;
        private System.Windows.Forms.TabPage pageFocusRects;
        private Panels.FocusRectsPanel panelFocusRects;
    }
}