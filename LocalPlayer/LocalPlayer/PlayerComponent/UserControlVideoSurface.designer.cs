﻿namespace LocalPlayer.PlayerComponent
{
    partial class UserControlVideoSurface
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // UserControlVideoSurface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.DoubleBuffered = true;
            this.Name = "UserControlVideoSurface";
            this.Size = new System.Drawing.Size(640, 480);
            this.Load += new System.EventHandler(this.UserControlVideoFrame_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UserControlVideoFrame_Paint);
            this.Move += new System.EventHandler(this.UserControlVideoFrame_Move);
            this.Resize += new System.EventHandler(this.UserControlVideoFrame_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
