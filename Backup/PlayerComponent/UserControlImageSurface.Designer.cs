namespace LocalPlayer.PlayerComponent
{
    partial class UserControlImageSurface
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
            this.components = new System.ComponentModel.Container();
            this.timerImg = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timerImg
            // 
            this.timerImg.Enabled = true;
            this.timerImg.Interval = 1000;
            this.timerImg.Tick += new System.EventHandler(this.timerImg_Tick);
            // 
            // UserControlImageSurface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Name = "UserControlImageSurface";
            this.Size = new System.Drawing.Size(640, 480);
            this.Load += new System.EventHandler(this.UserControlImageFrame_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UserControlImageFrame_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerImg;
    }
}
