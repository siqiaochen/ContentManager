using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using LocalPlayer.Controller;
using LocalPlayer.PlayElements;
using System.Threading;

namespace LocalPlayer
{
    public partial class DisplayForm : Form
    {
        private DisplayController displayController;
        public DisplayForm()
        {
            InitializeComponent();
            displayController = new DisplayController();
            InitScheduleAndPlay();
            this.SetDesktopBounds(0,0,20,20);
        }

        private void DisplayForm_Load(object sender, EventArgs e)
        {
            ucImageSurface.Dock = DockStyle.Fill;
            ucVideoSurface.Dock = DockStyle.Fill;
        }

        public void InitScheduleAndPlay()
        { 
            //displayController
            List<Schedule> schedules = new List<Schedule>();
            Schedule schedule = new Schedule();
            schedule.PlayItems.Add(new PlayItem("C:\\contents\\Custom_02_2012_64x128_1.avi", 0));
            schedule.PlayItems.Add(new PlayItem("C:\\contents\\Lotus.JPG", 1));
            schedules.Add(schedule);
            schedule = new Schedule();
            schedule.PlayItems.Add(new PlayItem("C:\\contents\\Catching.JPG", 1));
            schedule.PlayItems.Add(new PlayItem("C:\\contents\\Fairyland.JPG", 1));
            schedules.Add(schedule);
            displayController = new DisplayController(schedules);
            PlayItem item = displayController.ReadSchedule();
            if (item != null && File.Exists(item.Path))
                Play(item);
            else
                Thread.Sleep(100);
        }
        internal void Play(LocalPlayer.PlayElements.PlayItem item)
        {
            string ext = Path.GetExtension(item.Path);
            switch (ext.ToUpper())
            { 
                case ".AVI":
                case ".MP4":
                case ".MPG":
                    ucVideoSurface.SetDuration(item.Duration);
                    ucVideoSurface.PlayFile(item.Path);
                    ucVideoSurface.BringToFront();
                    break;
                case ".JPG":
                case ".PNG":
                case ".BMP":
                    ucImageSurface.SetDuration(item.Duration);
                    ucImageSurface.PlayFile(item.Path);
                    ucImageSurface.BringToFront();
                    break;
                default:
                    break;
            }
        }

        private void Surface_PlayFinishedEvent(object sender, EventArgs e)
        {
            PlayItem item = displayController.ReadSchedule();
            if (item != null && File.Exists(item.Path))
                Play(item);
            else
                Thread.Sleep(100);

        }

        private void setupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void notifyIconSettings_Click(object sender, EventArgs e)
        {
            contextMenuStripSettings.Show(Cursor.Position);
        }

        private void contextMenuStripSettings_Opening(object sender, CancelEventArgs e)
        {
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {

            PasswordInputForm passform = new PasswordInputForm();
            passform.Show();
        }



    }
}
