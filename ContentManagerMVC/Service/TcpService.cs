using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace ContentManagerMVC.Service
{
    public class TcpService
    {


        private TcpListener tcpListener;
        private Thread socketListenerThread;

        public TcpService()
        {
            this.tcpListener = new TcpListener(IPAddress.Any, 61111);
            this.socketListenerThread = new Thread(new ThreadStart(ListenOnSocket));
            this.socketListenerThread.Start();
        }



        private void ListenOnSocket()
        {
            this.tcpListener.Start();

            while (true)
            {
                //blocks until a client has connected to the server
                TcpClient client = this.tcpListener.AcceptTcpClient();

                //create a thread to handle communication 
                //with connected client
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientRequest));
                clientThread.Start(client);
            }
        }

        private void HandleClientRequest(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();
            ServerCommManager comm = new ServerCommManager();
            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 4096);
                    if (bytesRead > 0)
                    {
                        comm.ProcessReceivedBuffer(message, bytesRead);
                        if (comm.HaveResponse)
                        {
                            clientStream.Write(comm.ResponseBuf, 0, comm.ResponseBuf.Length);
                            clientStream.Flush();
                            comm.HaveResponse = false;
                        }
                    }
                }
                catch(Exception exp)
                {
                    //a socket error has occured
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    break;
                }

            }

            tcpClient.Close();
        }

    }
}