using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace ChatServer
{
    public class Server
    {
        #region Variablen
        private TcpListener listener;
        private Thread listenThread;
        public delegate void ClientHandler(IClient client);   
        public event ClientHandler NeuerClient;
        #endregion
        #region properties
        public IPAddress IPAdresse { get; private set; }
        public int Port { get; private set; }
        private bool Working { get; set; }
        #endregion
        #region Konstruktor
        public Server(IPAddress ipAdresse, int port)
        {     
            IPAddress ip = IPAddress.Any;

            if (ipAdresse != null)
                ip = ipAdresse;

            if (!(port > 0 && port <= 65535))
                throw new ArgumentOutOfRangeException("Kein Gültiger Port");

            IPAdresse = ip;
            Port = port;


            Working = true;
            this.listener = new TcpListener(IPAdresse, Port);
            this.listenThread = new Thread(new ThreadStart(HalteNachClientsAusschau));
            this.listenThread.Start(); 
        }
        #endregion
        #region private function
        private void HalteNachClientsAusschau()
        {
            this.listener.Start();
            
            while (Working)
            {
                try
                {
                    TcpClient client = this.listener.AcceptTcpClient();                    
                    var clientBenutzer = new ClientBenutzer(client);

                    if (NeuerClient != null)
                        NeuerClient(clientBenutzer);
                }
                catch (SocketException se)
                {
                    if (Working)
                    {
                        throw se;
                    }                   
                }
            }
        }
        
        private void HandleClientComm(object client)
        {
            Thread runningThread = Thread.CurrentThread;
            TcpClient tcpClient = (TcpClient)client;           
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[4096];
            int bytesRead;

            ASCIIEncoding encoder = new ASCIIEncoding(); 
            while (Working)
            {
                bytesRead = 0;

                try
                {
                    bytesRead = clientStream.Read(message, 0, 4096);                     
                }
                catch (System.IO.IOException)
                {
                    if (Working)
                    {
                        break;
                    }
                    else
                    {
                        break;
                    }
                }

                if (bytesRead == 0)
                    break;

                Console.WriteLine(encoder.GetString(message, 0, bytesRead));
                SendClientComm(tcpClient);
            }

          

            tcpClient.Close();            
        }        

        private void SendClientComm(TcpClient client)
        {
            Thread runningThread = Thread.CurrentThread;
            TcpClient tcpClient = client;
            NetworkStream clientStream = tcpClient.GetStream();

            string message = "Nachricht erhalten";

            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] buffer = encoder.GetBytes(message);

            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
        }
        #endregion

    }
}
