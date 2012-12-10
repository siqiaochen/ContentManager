using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace LocalPlayer
{
    class AccountHelper
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public int UpdateInterval { get; set; } //minutes
        private string SavePath = "system.sav";
        public AccountHelper()
        {
            Account = string.Empty;
            Password = string.Empty;
            UpdateInterval = 10;
            SavePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), SavePath);
        }
        public Boolean WriteToFile()
        {
            try
            {
                if (File.Exists(SavePath))
                {
                    File.Delete(SavePath);
                }
                using (FileStream fstream = new FileStream(SavePath, FileMode.CreateNew, FileAccess.Write))
                {
                    BinaryWriter bwriter = new BinaryWriter(fstream);
                    bwriter.Write(Account);
                    bwriter.Write(Password);
                    bwriter.Write(UpdateInterval);
                }
            }
            catch (Exception exp)
            {

            }
            return false;
        }
        public Boolean ReadFromFile()
        {
            try
            {
                if (!File.Exists(SavePath))
                {
                    return false;
                }
                using (FileStream fstream = new FileStream(SavePath, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader breader = new BinaryReader(fstream);
                    Account = breader.ReadString();
                    Password = breader.ReadString();
                    UpdateInterval = breader.ReadInt32();
                }
            }
            catch (Exception exp)
            {

            }
            return false;
        }
    }
}
