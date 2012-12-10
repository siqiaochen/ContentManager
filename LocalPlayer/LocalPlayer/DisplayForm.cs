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
using LocalPlayer.WCFCConnection;

namespace LocalPlayer
{
    public partial class DisplayForm : Form
    {
        private DisplayController displayController;
        private bool formClose;
        private string DownloadDir;
        private string ContentDir;
        private string ScheduleDir;
        public DisplayForm()
        {
            InitializeComponent();
            displayController = new DisplayController();
            InitScheduleAndPlay();
            this.SetDesktopBounds(0,0,20,20);
            formClose = false;
            DownloadDir = Path.Combine(Application.ExecutablePath, "Download\\");
            ContentDir = Path.Combine(Application.ExecutablePath, "Content\\");
            ScheduleDir = Path.Combine(Application.ExecutablePath, "Schedule\\"); 
        }
        private bool downloadAllFiles(List<Schedule> Schedules, WCFClient client)
        {
            List<string> downloadedItems = new List<string>();
            try
            {
                foreach (Schedule sche in Schedules)
                {
                    foreach (PlayItem item in sche.PlayItems)
                    {
                        string dstpath = Path.Combine(ContentDir, Path.GetFileName(item.Path));
                        if (!File.Exists(dstpath))
                        {
                            string downloadpath = Path.Combine(DownloadDir, Path.GetFileName(item.Path) + ".down");
                            if (client.DownloadFile(item.Path, downloadpath))
                            {
                                if (File.Exists(dstpath))
                                {
                                    File.Move(downloadpath, dstpath);
                                    downloadedItems.Add(dstpath);
                                }
                            }
                        }
                        Thread.Sleep(1000);
                    }

                }
            }
            catch (Exception exp)
            { }
            return false;
        }
        private void ReceiveInformation()
        {

            while (!formClose)
            {
                try {
                    
                    string UserName="";
                    string Password="";
                    int waittime = 0;
                    AccountHelper accounthelper = new AccountHelper();
                    if (accounthelper.ReadFromFile())
                    {
                        UserName = accounthelper.Account;
                        Password = accounthelper.Password;
                        waittime = accounthelper.UpdateInterval;

                        WCFClient client = new WCFClient(UserName, Password);
                        int count = client.CheckScheduleCount();
                        List<Schedule> schedules = new List<Schedule>();
                        if (count >= 0)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                Schedule sche = null;
                                if (sche != null)
                                    client.CheckSchedule(i);
                                schedules.Add(sche);
                            }
                            if (downloadAllFiles(schedules, client))
                            {
                                DisplayController newController = new DisplayController(schedules);
                                String schepath = Path.Combine(ScheduleDir, "Schedule.xml");
                                String oldschepath = Path.Combine(ScheduleDir, DateTime.Now.ToString("MMddyy_Hmmss")+"Schedule.old");
                                try
                                {
                                    if (File.Exists(oldschepath))
                                    {
                                        File.Delete(oldschepath);
                                    }
                                    File.Move(schepath, oldschepath);
                                    newController.WriteToXML(schepath);
                                    // better make a lock here
                                }
                                catch (Exception exp)
                                { 
                                
                                }
                            }
                        }
                        DateTime time = DateTime.Now;
                        while (DateTime.Now.Subtract(time).TotalMinutes < waittime)
                        {
                            Thread.Sleep(100);
                        }
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception exp)
                { 
                }
            }
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
            schedule.PlayItems.Add(new PlayItem("1","C:\\contents\\Custom_02_2012_64x128_1.avi", 0));
            schedule.PlayItems.Add(new PlayItem("2","C:\\contents\\Lotus.JPG", 1));
            schedules.Add(schedule);
            schedule = new Schedule();
            schedule.PlayItems.Add(new PlayItem("3","C:\\contents\\Catching.JPG", 1));
            schedule.PlayItems.Add(new PlayItem("4","C:\\contents\\Fairyland.JPG", 1));
            schedules.Add(schedule);
            displayController = new DisplayController(schedules);
            PlayItem item = displayController.GetNextFileFromSchedule();
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
            PlayItem item = displayController.GetNextFileFromSchedule();
            while (item == null || !File.Exists(item.Path))
            {
                Thread.Sleep(100);
                item = displayController.GetNextFileFromSchedule();
            }
            Play(item);
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
