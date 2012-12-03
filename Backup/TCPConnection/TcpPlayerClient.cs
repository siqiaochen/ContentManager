using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace LocalPlayer.TCPConnection
{
    class TcpPlayerClient
    {
        private TcpClient client;
        public string URL {get;set;}
        public int Port { get; set; }
        private PlayerCommManager playerMgr;
        private NetworkStream clientStream;
        public string User { get; set; }
        public string Password { get; set; }
        public TcpPlayerClient(string url, int port)
        {
            URL = url;
            Port = port;
            playerMgr = new PlayerCommManager();
        }
        public void Connect()
        {

            client = new TcpClient();
            //IPHostEntry host;

            //host = Dns.GetHostEntry(URL);
            client.Connect( URL, Port);
            client.ReceiveTimeout = 500;
            clientStream = client.GetStream();
        }

        public bool Login()
        {
            return playerMgr.SendLoginCommand(clientStream,User,Password);
        }
        public void Release()
        {
            if (client != null)
            {
                client.Close();
            }
        }

    }
}
