using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using NextGen.Utlilities;

namespace LocalPlayer.TCPConnection
{

    public class PlayerCommManager
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
        private byte[] rxBuf;
        private byte[] receivedBuf;
        private byte crc_1;
        private byte crc_2;
        ASCIIEncoding ascEncoding;
        public delegate string ResponseEvent(byte[] ans);
        public static event ResponseEvent OnResponseEvent;
        private bool ansReceived;
        private byte[] ResponseBuf;


        public PlayerCommManager()
        {
            isAuthorized = false;
            ascEncoding = new ASCIIEncoding();
            ansReceived = false;
            rxBuf = new byte[4096];
        }
        /*
        public void fireResponseEvent(byte[] ans)
        {
            if (OnResponseEvent != null)
                OnResponseEvent(ans);
        }
        */
        public bool SendLoginCommand(NetworkStream netstream, string user, string pass)
        {
            if (netstream == null)
                return false;
            if (user.Length > 255 || pass.Length > 255)
                return false;
            byte[] req = new Byte[4096];
            ushort index = 0;
            req[index++] = cmd_PlayerLogin;
            byte[] userbuf = ascEncoding.GetBytes(user);
            byte[] passbuf = ascEncoding.GetBytes(pass);
            req[index++] = (byte)userbuf.Length;
            Array.Copy(userbuf, 0, req, index, userbuf.Length);
            index += (ushort)userbuf.Length;
            req[index++] = (byte)passbuf.Length;
            Array.Copy(passbuf, 0, req, index, passbuf.Length);
            index += (ushort)passbuf.Length;
            byte[] pkg = new byte[index];
            Array.Copy(req, pkg, index);
            req = BuildCmdPackage(pkg);
            
            try
            {

                netstream.Write(req, 0, req.Length);
                netstream.Flush();


                int bytesRead = netstream.Read(rxBuf, 0, 4096);
                if (bytesRead > 0)
                {
                    ProcessReceivedBuffer(rxBuf, bytesRead);
                    if (ansReceived == true)
                    {
                        if(processLoginCmd())
                            isAuthorized = true;
                    }

                    ansReceived = false;
                }
            }
            catch(Exception exp)
            {
                //a socket error has occured
                //break;
            }
            
            return false;
        }
        public void resetProcessStatus()
        {
            currCmdProcess = 0;
            currCmdSubProcess = 0;
            packlen_1 = 0;
            packlen_2 = 0;
            crc_1 = 0;
            crc_2 = 0;
        }

        private bool processLoginCmd()
        {
            if (receivedBuf[0] == cmd_PlayerLogin)
                if (receivedBuf[1] == ans_ok)
                {
                    return true;
                }
            return false;
        }
        public void ProcessANS()
        {
            ansReceived = true;
        }
        public byte[] BuildCmdPackage(byte[] buf)
        {
            byte[] pack = new byte[buf.Length + 8];
            pack[0] = (byte)'R';
            pack[1] = (byte)'E';
            pack[2] = (byte)'Q';
            pack[3] = (byte)(buf.Length % 0x100);
            pack[4] = (byte)(buf.Length / 0x100);
            pack[5] = (byte)(pack[3] << 1 + pack[4]);
            Array.Copy(buf, 0, pack, 6, buf.Length);
            Crc16Ccitt crc = new Crc16Ccitt(Crc16Ccitt.InitialCrcValue.Zeros);
            byte[] crcbuf = crc.ComputeChecksumBytes(buf);
            Array.Copy(crcbuf, 0, pack, buf.Length + 6, 2);
            return pack;
        }
        public bool ProcessReceivedBuffer(byte[] buff,int length)
        {
            for (int bufIndex = 0; bufIndex < length; bufIndex++)
            {
                switch (currCmdProcess)
                {
                    case 0:
                        switch (currCmdSubProcess)
                        {
                            case 0:
                                if (buff[bufIndex] == 'A')
                                    currCmdSubProcess++;
                                else
                                    resetProcessStatus();
                                break;
                            case 1:
                                if (buff[bufIndex] == 'S')
                                    currCmdSubProcess++;
                                else
                                    resetProcessStatus();
                                break;
                            case 2:
                                if (buff[bufIndex] == 'N')
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
                                    packagelen = (ushort)(packlen_1 + packlen_2 * 0x100);
                                    currCmdSubProcess = 0;
                                    currCmdProcess++;
                                }
                                else
                                    resetProcessStatus();
                                break;

                        }
                        break;
                    case 1:
                        if (currCmdSubProcess == 0)
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
                                ProcessANS();
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
