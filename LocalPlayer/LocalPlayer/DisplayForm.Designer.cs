namespace LocalPlayer
{
    partial class DisplayForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayForm));
            this.notifyIconSettings = new System.Windows.Forms.NotifyIcon(this.components);
            this.ucVideoSurface = new LocalPlayer.PlayerComponent.UserControlVideoSurface();
            this.ucImageSurface = new LocalPlayer.PlayerComponent.UserControlImageSurface();
            this.contextMenuStripSettings = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIconSettings
            // 
            this.notifyIconSettings.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconSettings.Icon")));
            this.notifyIconSettings.Text = "Player Settings";
            this.notifyIconSettings.Visible = true;
            this.notifyIconSettings.Click += new System.EventHandler(this.notifyIconSettings_Click);
            // 
            // ucVideoSurface
            // 
            this.ucVideoSurface.BackColor = System.Drawing.Color.Black;
            this.ucVideoSurface.Location = new System.Drawing.Point(35, 156);
            this.ucVideoSurface.Name = "ucVideoSurface";
            this.ucVideoSurface.Size = new System.Drawing.Size(151, 109);
            this.ucVideoSurface.TabIndex = 1;
            this.ucVideoSurface.PlayFinishedEvent += new LocalPlayer.PlayerComponent.PlayFinishedEventHandler(this.Surface_PlayFinishedEvent);
            // 
            // ucImageSurface
            // 
            this.ucImageSurface.BackColor = System.Drawing.Color.Black;
            this.ucImageSurface.Location = new System.Drawing.Point(35, 27);
            this.ucImageSurface.Name = "ucImageSurface";
            this.ucImageSurface.Size = new System.Drawing.Size(151, 109);
            this.ucImageSurface.TabIndex = 0;
            this.ucImageSurface.PlayFinishedEvent += new LocalPlayer.PlayerComponent.PlayFinishedEventHandler(this.Surface_PlayFinishedEvent);
            // 
            // contextMenuStripSettings
            // 
            this.contextMenuStripSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingToolStripMenuItem});
            this.contextMenuStripSettings.Name = "contextMenuStripSettings";
            this.contextMenuStripSettings.Size = new System.Drawing.Size(153, 48);
            this.contextMenuStripSettings.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripSettings_Opening);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.settingToolStripMenuItem.Text = "Setting";
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.settingToolStripMenuItem_Click);
            // 
            // DisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(411, 266);
            this.Controls.Add(this.ucVideoSurface);
            this.Controls.Add(this.ucImageSurface);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DisplayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Main";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.DisplayForm_Load);
            this.contextMenuStripSettings.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private LocalPlayer.PlayerComponent.UserControlImageSurface ucImageSurface;
        private LocalPlayer.PlayerComponent.UserControlVideoSurface ucVideoSurface;
        private System.Windows.Forms.NotifyIcon notifyIconSettings;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSettings;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
    }
}

