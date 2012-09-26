using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace ChatServer
{
    public abstract class Server
    {
        #region Variablen
        private TcpListener listener;
        private Thread listenThread;
        #endregion
        #region properties
        public IPAddress IPAdresse { get; private set; }
        public int Port { get; private set; }
        public bool Working { get; private set; }
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
        #region Öffentliche Funktionen
        public void Stop()
        {
            Working = false;
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
                    ClientGefunden(client);                    
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
        internal virtual void ClientGefunden(TcpClient tcpClient)
        {
        }
        #endregion

    }
}
