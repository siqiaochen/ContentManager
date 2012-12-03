using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NextGen.Utlilities;
using System.Text;
using ContentManagerMVC.Models;

namespace ContentManagerMVC.Service
{
    public class ServerCommManager
    {
        public byte cmd_PlayerLogin = 1;
        public byte cmd_DownloadSchedule = 2;
        public byte cmd_DownloadFiles = 3;
        public byte ans_ok = 0x1;
        public byte ans_error = 0x0;

        public bool isAuthorized { get; set; }
        private int currCmdProcess;
        private int currCmdSubProcess;
        private byte packlen_1;
        private byte packlen_2;
        private ushort packagelen;
        private byte[] receivedBuf;
        private byte crc_1;
        private byte crc_2;
        ASCIIEncoding ascEncoding;
        public delegate string ResponseEvent(byte[] ans);
        public static event ResponseEvent OnResponseEvent;
        private PlayerDBContext db;
        public bool HaveResponse { get; set; }
        public byte[] ResponseBuf;
        public ServerCommManager()
        {
            isAuthorized = false;
            ascEncoding = new ASCIIEncoding();
            db = new PlayerDBContext();
            HaveResponse = false;
        }
        /*
        public void fireResponseEvent(byte[] ans)
        {
            if (OnResponseEvent != null)
                OnResponseEvent(ans);
        }
        */
        public void resetProcessStatus()
        {
            currCmdProcess = 0;
            currCmdSubProcess = 0;
            packlen_1 = 0;
            packlen_2 = 0;
            crc_1 = 0;
            crc_2 = 0;
        }
        public void ProcessCMD()
        {
            if (receivedBuf != null)
            {
                
                if (isAuthorized)
                {
                    
                }
                else
                {
                    if (receivedBuf[0] != cmd_PlayerLogin)
                        return;
                    else
                    { 
                        int index = 1;
                        int namelen = receivedBuf[index++];
                        string name = new string(ascEncoding.GetChars(receivedBuf, index, namelen));
                        index += namelen;
                        int passlen = receivedBuf[index++];
                        string pass = new string(ascEncoding.GetChars(receivedBuf, index, passlen));
                        processLoginCmd(name,pass);
                    }
                }

            }
        }
        private void processLoginCmd(string user,string pass)
        {
            var players = from s in db.Players
                          select s;
            if (players.Count() > 0)
            {
                Player player = players.First(s => s.Name.Equals(user));
                if (pass == "123456")
                {
                    isAuthorized = true;
                    byte[] ans = new byte[2];
                    ans[0] = cmd_PlayerLogin;
                    ans[1] = ans_ok;
                    BuildAnsPackage(ans);
                }
                else
                {
                    byte[] ans = new byte[2];
                    ans[0] = cmd_PlayerLogin;
                    ans[1] = ans_error;
                    BuildAnsPackage(ans);
                }
            }
            else
            {
                byte[] ans = new byte[2];
                ans[0] = cmd_PlayerLogin;
                ans[1] = ans_error;
                BuildAnsPackage(ans);
            }
        }
        public void BuildAnsPackage(byte[] buf)
        {
            byte[] pack = new byte[buf .Length+ 8];
            pack[0] = (byte)'A';
            pack[1] = (byte)'S';
            pack[2] = (byte)'N';
            pack[3] = (byte)(buf.Length % 0x100);
            pack[4] = (byte)(buf.Length / 0x100);
            pack[5] = (byte)(pack[3]<<1+pack[4]);
            Array.Copy(buf, 0, pack, 6, buf.Length);
            Crc16Ccitt crc = new Crc16Ccitt(Crc16Ccitt.InitialCrcValue.Zeros);
            byte[] crcbuf= crc.ComputeChecksumBytes(buf);
            Array.Copy(crcbuf, 0, pack, buf.Length + 6, 2);
            ResponseBuf = pack;
            HaveResponse = true;
        }
        public bool ProcessReceivedBuffer(byte[] buff,int length)
        {
            for (int bufIndex = 0; bufIndex < length; bufIndex++)
            {
                switch (currCmdProcess)
                { 
                    case 0:

                        HaveResponse = false;
                        switch(currCmdSubProcess)
                        {
                            case 0:
                                if (buff[bufIndex] == 'R')
                                    currCmdSubProcess++;
                                else
                                    resetProcessStatus();
                                break;
                            case 1:
                                if (buff[bufIndex] == 'E')
                                    currCmdSubProcess++;
                                else
                                    resetProcessStatus();                                
                                break;
                            case 2:
                                if (buff[bufIndex] == 'Q')
                                    currCmdSubProcess++;
                                else
                                    resetProcessStatus();
                                break;
                            case 3:
                                    packlen_1 = buff[bufIndex];
                                    currCmdSubProcess++;
                                break;
                            case 4:
                                    packlen_2 = buff[bufIndex];
                                    currCmdSubProcess++;
                                    break;
                            case 5:
                                    if (buff[bufIndex] == (byte)(packlen_1 << 1 + packlen_2))
                                    {
                                        packagelen = (ushort)(packlen_1+packlen_2*0x100);
                                        currCmdSubProcess = 0;
                                        currCmdProcess++;
                                    }
                                    else
                                        resetProcessStatus();
                                break;
    
                        }
                        break;
                    case 1:

                        HaveResponse = false;
                        if(currCmdSubProcess==0)
                            receivedBuf = new byte[packagelen];
                        if (currCmdSubProcess < packagelen)
                        {
                            receivedBuf[currCmdSubProcess++] = buff[bufIndex];
                            if (currCmdSubProcess >= packagelen)
                            {
                                currCmdProcess++;
                                currCmdSubProcess = 0;
                            }
                        }
                        else
                        {
                            resetProcessStatus();
                        }
                        break;
                    case 2:
                        if (currCmdSubProcess == 0)
                        {
                            crc_1 = buff[bufIndex];
                            currCmdSubProcess++;
                        }
                        else
                        {
                            crc_2 = buff[bufIndex];
                            Crc16Ccitt crc = new Crc16Ccitt(Crc16Ccitt.InitialCrcValue.Zeros);
                            byte[] bCrc = crc.ComputeChecksumBytes(receivedBuf);
                            if (bCrc[0] == crc_1 && bCrc[1] == crc_2)
                            {
                                ProcessCMD();
                            }
                            resetProcessStatus();
                        }
                        break;
                }
            }
            return false;
        }


    }
}