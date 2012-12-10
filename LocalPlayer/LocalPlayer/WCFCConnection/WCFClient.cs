using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.IO;
using System.ServiceModel.Channels;
using System.Security.Cryptography;
using System.Windows.Forms;
using LocalPlayer.WCF;
using LocalPlayer.PlayElements;

namespace LocalPlayer.WCFCConnection
{
    class WCFClient
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public WCFClient(string user, string pwd)
        {
            Username = user;
            Password = pwd;
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
        private void SetHeader(OperationContextScope scope)
        {


            string authToken = Username+":"+Password+"@" + DateTime.UtcNow.ToString("MM/dd/yy-H:mm:ss");
            MessageHeader myHeader1 = MessageHeader.CreateHeader(
                 "PLAYER", "http://my", Username);
            MessageHeader myHeader2 = MessageHeader.CreateHeader(
                "TIME", "http://my", DateTime.UtcNow.ToString("MM/dd/yy-H:mm:ss"));
            MessageHeader myHeader3 = MessageHeader.CreateHeader(
                "TOKEN", "http://my", CalculateMD5Hash(authToken));
            OperationContext.Current.OutgoingMessageHeaders.Add(myHeader1);
            OperationContext.Current.OutgoingMessageHeaders.Add(myHeader2);
            OperationContext.Current.OutgoingMessageHeaders.Add(myHeader3);

        }
        public int CheckScheduleCount()
        {        
            WCF.PlayerServiceClient client = new WCF.PlayerServiceClient();
            try
            {
                
                using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
                {

                    SetHeader(scope);

                    int result =  client.CheckScheduleCount();

                    client.Close();
                    return result;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            return -1;
        }
        public bool DownloadFile(string srcFile,string dstFile)
        {
            
            WCF.PlayerServiceClient client = new WCF.PlayerServiceClient();
            try
            {
                
                using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
                {

                    SetHeader(scope);
                    DlFileInfo dfinfo = new DlFileInfo();
                    dfinfo.Name = srcFile;
                    dfinfo.Length = 1000000;

                    dfinfo.StartPoint = 0;
                    long filelen = -1;
                    using (FileStream fstream = new FileStream(dstFile, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        fstream.SetLength(0);
                        while (dfinfo.StartPoint != filelen)
                        {
                            DlFileBuff buf = (client.DownloadFile(dfinfo));
                            if (buf == null)
                            {
                                throw new System.Exception("WCF File Download: Empty Buffer. It appears that file doesn't exist on server");
                            }
                            int len = (int)(buf.CurrPoint - dfinfo.StartPoint);
                            if (len > 0)
                            {
                                Byte[] barr = new Byte[4096];
                                fstream.Write(buf.BufStream, 0, len);
                                /*
                                while (buf.BufStream.Position <( len-1))
                                {
                                    int bufread = buf.BufStream.Read(barr, 0, 4096);
                                    fstream.Write(barr, 0, bufread);
                                }
                                */
                            }
                            dfinfo.StartPoint = buf.CurrPoint;
                            filelen = buf.FileSize;
                            Application.DoEvents();
                            Application.DoEvents();

                            client.Close();
                            //Thread.Sleep(100);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return false;
            }
            return true;
        }

        internal PlayElements.Schedule CheckSchedule(int i)
        {
            WCF.PlayerServiceClient client = new WCF.PlayerServiceClient();
            try
            {

                using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
                {

                    SetHeader(scope);

                    string xmlstr = client.DownloadSchedule(i);
                    Schedule sche = ContentHelper.XMLStringToSchedule(xmlstr);
                    return sche;
                }

                client.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            return null;
        }
    }
}
