using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using ContentManagerMVC.Models;
using System.Web;
using System.Web.Mvc;
namespace ContentManagerMVC.WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PlayerService" in code, svc and config file together.
    public class PlayerService : IPlayerService
    {
        int count = 0;
        private bool CheckAuthInfo()
        {
            try
            {
                string PlayerName = string.Empty, Date = string.Empty, HASH = string.Empty;

                int index = OperationContext.Current.IncomingMessageHeaders.FindHeader("PLAYER", "http://my");
                if (index != -1)
                {
                    PlayerName = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>(index);
                }
                index = OperationContext.Current.IncomingMessageHeaders.FindHeader("TIME", "http://my");
                if (index != -1)
                {
                    Date = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>(index);
                }
                index = OperationContext.Current.IncomingMessageHeaders.FindHeader("TOKEN", "http://my");
                if (index != -1)
                {
                    HASH = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>(index);
                }
                if (PlayerName != string.Empty&&Date != string.Empty && HASH!=string.Empty)
                {
                    string pwd = string.Empty;
                    PlayerDBContext db = new PlayerDBContext();
                    var players = from s in db.Players
                                  select s;

                    var player = players.First(s => s.Name.Equals(PlayerName));
                    pwd = player.Password;
                    if (pwd != string.Empty)
                    {
                        string authToken = CalculateMD5Hash(PlayerName + ":" + pwd + "@" + Date);
                        if (string.Compare(authToken, HASH) == 0)
                            return true;
                    }
                }
            }
            catch (Exception exp)
            { }
            return false;
        }
        public string DoWork()
        {
            if (CheckAuthInfo())
            {
                return "First Test!";
            }
            return "Authentication Error";

        }
        public DlFileBuff DownloadFile(DlFileInfo FileQuery)
        {
            if (CheckAuthInfo())
            {
                DlFileBuff fileBuf = new DlFileBuff();
                //string filename = Path.Combine(Server.MapPath("~/App_Data/"), FileQuery.Name);
                using (FileStream fstream = new FileStream(FileQuery.Name, FileMode.Open, FileAccess.Read))
                {
                    fileBuf.FileSize = fstream.Length;
                    fileBuf.Name = FileQuery.Name;
                    byte[] barray = new byte[FileQuery.Length];
                    fstream.Seek(FileQuery.StartPoint, SeekOrigin.Begin);
                    int len = fstream.Read(barray, 0, FileQuery.Length);
                    if (len > 0)
                    {
                        fileBuf.BufStream = barray;
                        fileBuf.CurrPoint = FileQuery.StartPoint + len;
                    }
                }
                return fileBuf;
            }
            return null;

        }
        public string DownloadSchedule(int i) 
        {
            if (CheckAuthInfo())
            {

                int index = OperationContext.Current.IncomingMessageHeaders.FindHeader("PLAYER", "http://my");
                if (index != -1)
                {
                    string PlayerName = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>(index);
                    PlayerDBContext db = new PlayerDBContext();
                    var players = from s in db.Players
                                  select s;

                    var player = players.First(s => s.Name.Equals(PlayerName));
                    if (player.Schedules.Count > i)
                    {
                        Schedule sche = player.Schedules.ElementAt(i).schedule;
                        string xml = ContentHelper.ScheduleToXMLString(sche);
                        return xml;
                    }
                }
            }
            return null;
        }

        public int CheckScheduleCount()
        {
            if (CheckAuthInfo())
            {

                int index = OperationContext.Current.IncomingMessageHeaders.FindHeader("PLAYER", "http://my");
                if (index != -1)
                {
                    string PlayerName = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>(index);
                    PlayerDBContext db = new PlayerDBContext();
                    var players = from s in db.Players
                                  select s;

                    var player = players.First(s => s.Name.Equals(PlayerName));
                    return player.Schedules.Count();
                }
            }
            return -1;
        }
        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        public string AddInteger(int i)
        {
            count += i;
            return "count = " + count;
        }
    }
}
