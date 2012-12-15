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
        private bool newScheUpdated;
        private string DownloadDir;
        private string ContentDir;
        private string ScheduleDir;
        private enum PlayerStatus { Init, Playing, Finished }
        private PlayerStatus Status { get; set; }
        Thread workthread;
        public DisplayForm()
        {
            Status = PlayerStatus.Init;
            InitializeComponent();
            this.SetDesktopBounds(0,0,256,128);
            formClose = false;
            newScheUpdated = false;
            DownloadDir = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Download\\");
            if (!Directory.Exists(DownloadDir))
                Directory.CreateDirectory(DownloadDir);
            ContentDir = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Content\\");
            if (!Directory.Exists(ContentDir))
                Directory.CreateDirectory(ContentDir);
            ScheduleDir = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Schedule\\");
            if (!Directory.Exists(ScheduleDir))
                Directory.CreateDirectory(ScheduleDir);
            workthread = new Thread(new ThreadStart(ReceiveInformation));
            workthread.Start();

            displayController = new DisplayController();
            InitScheduleAndPlay();
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
                        string svrpath = Path.Combine(ContentDir, Path.GetFileName(item.Path));
                        item.Path = dstpath;
                        try
                        {
                            if (!File.Exists(dstpath))
                            {
                                string downloadpath = Path.Combine(DownloadDir, Path.GetFileName(item.Path) + ".down");
                                if (client.DownloadFile(svrpath, downloadpath))
                                {
                                    if (File.Exists(downloadpath))
                                    {
                                        File.Move(downloadpath, dstpath);
                                        downloadedItems.Add(dstpath);
                                    }
                                }
                            }
                        }
                        catch (Exception exp)
                        { 
                        
                        }
                        Thread.Sleep(1000);
                    }

                }
                return true;
            }
            catch (Exception exp)
            { }
            return false;
        }
        // Copied from stackoverflow
        private bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;

            // Determine if the same file was referenced two times.
            if (file1 == file2)
            {
                // Return true to indicate that the files are the same.
                return true;
            }

            // Open the two files.
            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);

            // Check the file sizes. If they are not the same, the files 
            // are not the same.
            if (fs1.Length != fs2.Length)
            {
                // Close the file
                fs1.Close();
                fs2.Close();

                // Return false to indicate files are different
                return false;
            }

            // Read and compare a byte from each file until either a
            // non-matching set of bytes is found or until the end of
            // file1 is reached.
            do
            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            // Close the files.
            fs1.Close();
            fs2.Close();

            // Return the success of the comparison. "file1byte" is 
            // equal to "file2byte" at this point only if the files are 
            // the same.
            return ((file1byte - file2byte) == 0);
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
                                Schedule sche = client.CheckSchedule(i);
                                if (sche != null)
                                    schedules.Add(sche);
                            }
                            if (downloadAllFiles(schedules, client))
                            {
                                DisplayController newController = new DisplayController(schedules);
                                String tmppath = Path.Combine(DownloadDir, "Schedule.xml");
                                String schepath = Path.Combine(ScheduleDir, "Schedule.xml");
                                String oldschepath = Path.Combine(ScheduleDir, DateTime.Now.ToString("MMddyy_Hmmss")+"Schedule.old");
                                try
                                {
                                    if (File.Exists(oldschepath))
                                    {
                                        File.Delete(oldschepath);
                                    }
                                    if (File.Exists(tmppath))
                                    {
                                        File.Delete(tmppath);
                                    }
                                    newController.WriteToXML(tmppath);
                                    if (!FileCompare(tmppath, schepath))
                                    {
                                        if (File.Exists(schepath))
                                            File.Move(schepath, oldschepath);
                                        File.Move(tmppath, schepath);
                                        newScheUpdated = true;
                                    }


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
                            if (formClose)
                                return;
                        }
                    }
                    else
                    {
                        Thread.Sleep(10000);
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

            String schepath = Path.Combine(ScheduleDir, "Schedule.xml");
            displayController = new DisplayController();
            displayController.ReadFromXML(schepath);
            Status = PlayerStatus.Finished;
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
                    Status = PlayerStatus.Playing;
                    break;
                case ".JPG":
                case ".PNG":
                case ".BMP":
                    ucImageSurface.SetDuration(item.Duration);
                    ucImageSurface.PlayFile(item.Path);
                    ucImageSurface.BringToFront();
                    Status = PlayerStatus.Playing;
                    break;
                default:
                    break;
            }
        }

        private void Surface_PlayFinishedEvent(object sender, EventArgs e)
        {
            Status = PlayerStatus.Finished;
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

        private void timerSchedule_Tick(object sender, EventArgs e)
        {
            if (Status == PlayerStatus.Playing || Status == PlayerStatus.Init)
            {
                return;
            }
            if (newScheUpdated)
            {
                displayController = new DisplayController();
                String schepath = Path.Combine(ScheduleDir, "Schedule.xml");
                displayController.ReadFromXML(schepath);
                newScheUpdated = false;
            }
            PlayItem item = displayController.GetNextFileFromSchedule();
            if (item != null && File.Exists(item.Path))
                Play(item);
        }

        private void DisplayForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            formClose = true;
        }



    }
}
