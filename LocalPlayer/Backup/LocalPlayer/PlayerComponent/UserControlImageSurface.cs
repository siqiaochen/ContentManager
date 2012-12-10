using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LocalPlayer.PlayerComponent
{
    public partial class UserControlImageSurface : UserControl, IFrameControl
    {
        internal enum PlayState
        {
            Stopped,
            Running,
            Init
        };
        private PlayState state;
        private Image image;
        private int tickcount = 0;
        private int duration = 10;
        public event PlayFinishedEventHandler PlayFinishedEvent;
        public UserControlImageSurface()
        {
            InitializeComponent();

            state = PlayState.Init;
        }
        public void PlayFile(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    if (PlayFinishedEvent != null)
                    {
                        PlayFinishedEvent(this, EventArgs.Empty);
                    }
                }
                image = new Bitmap(path);
                state = PlayState.Running;
                tickcount = 0;
                Refresh();
            }
            catch (Exception exp)
            {

                if (PlayFinishedEvent != null)
                {
                    PlayFinishedEvent(this, EventArgs.Empty);
                }
            
            }
        }
        public void Replay()
        {
            tickcount = 0;
            if (image != null)
                state = PlayState.Running;        
        }
        public void Stop()
        {
            state = PlayState.Stopped;
        }
        public void SetDuration(int secs)
        {
            duration = secs;
        }
        public void ReleaseResources()
        {
            image.Dispose();
        }
        private void timerImg_Tick(object sender, EventArgs e)
        {
            if (state == PlayState.Running)
            {
                tickcount++;
                if (tickcount > duration)
                {
                    state = PlayState.Stopped;
                    if (PlayFinishedEvent != null)
                    {
                        PlayFinishedEvent(this, EventArgs.Empty);
                    }
                }
            }
        }

        private void UserControlImageFrame_Load(object sender, EventArgs e)
        {
        }

        private void UserControlImageFrame_Paint(object sender, PaintEventArgs e)
        {
            if (state == PlayState.Running)
                e.Graphics.DrawImage(image, 0, 0, this.Width, this.Height);
        }
    }
}
