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
        private TcpListener tcpListener;
        private Thread listenTread;        
        public event EventHandler ClientDisconnected;
        public event EventHandler ClientConnected;
        public event EventHandler ServerMessage;
        Dictionary<Thread,int> ConnectedThreads;
        Dictionary<Thread, NetworkStream> ConnectedStreams;
        int ThreadClientID = 1;                
        #endregion
        #region properties
        public int ConnectedThreadsCount 
        {
            get
            {
                if (ConnectedThreads == null)
                    return 0;
                else
                    return ConnectedThreads.Count;
            }
        }
        public DateTime Startup { get; private set; }
        public DateTime UpTime 
        {
            get
            {  
                DateTime realuptime = new DateTime();                
                return realuptime.AddSeconds(DateTime.Now.Subtract(Startup).Seconds);
            }
        }
        public IPAddress IPAdresse { get; private set; }
        public int Port { get; private set; }
        private bool Working { get; set; }
        #endregion
        #region Konstruktor
        /// <summary>
        /// IPAdresse -> Any
        /// Port -> 3000
        /// </summary>
        public Server()
        {
            IPAdresse = IPAddress.Any;
            Port = 3000;

            konst();       
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip">IP Adresse</param>
        /// <param name="port">Port</param>
        public Server(IPAddress ip, int port)
        {
            IPAdresse = ip;
            Port = port;

            konst();                
        }
        private void konst()
        {
            Working = true;
            Startup = DateTime.Now;
            WriteHeader();
            this.tcpListener = new TcpListener(IPAdresse, Port);
            this.listenTread = new Thread(new ThreadStart(ListenForClients));
            this.listenTread.Start(); 
        }
        #endregion
        #region private function
        private void ListenForClients()
        {
            this.tcpListener.Start();
            ConnectedThreads = new Dictionary<Thread,int>();
            ConnectedStreams = new Dictionary<Thread, NetworkStream>();
            
            while (Working)
            {
                try
                {
                    TcpClient client = this.tcpListener.AcceptTcpClient();
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                    ConnectedThreads.Add(clientThread,ThreadClientID);
                    ThreadClientID++;

                    if (ClientConnected != null)
                        ClientConnected(ConnectedThreads[clientThread], null);
                    clientThread.Start(client);
                }
                catch (SocketException se)
                {
                    if (Working)
                    {
                        throw se;
                    }
                    else
                        WriteServerMessage("- TCP-Listener gestoppt -",MessageStatus.StatusMessage);
                }
            }
        }
        private void HandleClientComm(object client)
        {
            Thread runningThread = Thread.CurrentThread;
            TcpClient tcpClient = (TcpClient)client;           
            NetworkStream clientStream = tcpClient.GetStream();
            ConnectedStreams.Add(runningThread, clientStream);
            
            

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
                        WriteServerMessage("- Client (" + ConnectedThreads[runningThread] + ") gestoppt -",MessageStatus.StatusMessage);
                        break;
                    }
                }

                if (bytesRead == 0)
                    break;

                WriteServerMessage("Communication (Client: " + ConnectedThreads[runningThread] + ") to Server: " + encoder.GetString(message, 0, bytesRead),MessageStatus.ClientToServer);
                SendClientComm(tcpClient);
            }

            if (ClientDisconnected != null)
                ClientDisconnected(ConnectedThreads[runningThread], null);
            
            ConnectedThreads.Remove(runningThread);
            ConnectedStreams.Remove(runningThread);

            tcpClient.Close();            
        }        
        private void WriteHeader()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("/******************************************\\");
            sb.AppendLine("|*********** Verwaltungs-Server ***********|");
            sb.AppendLine("\\******************************************/");

            WriteServerMessage(sb.ToString(),MessageStatus.HeaderMessage);
        }
        private void WriteServerMessage(string msg, MessageStatus Status)
        {
            int statusID = (int)Status;
            Console.WriteLine(msg);

            if (ServerMessage != null)
                ServerMessage(msg, null);
        }
        private void SendClientComm(TcpClient client)
        {
            Thread runningThread = Thread.CurrentThread;
            TcpClient tcpClient = client;
            NetworkStream clientStream = tcpClient.GetStream();

            string message = "Nachricht erhalten";

            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] buffer = encoder.GetBytes(message);

            WriteServerMessage("Communication Server to (Client: " + ConnectedThreads[runningThread] + "): " + message,MessageStatus.ServerToClient);

            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
        }
        #endregion
        #region public function
        public void stopServer()
        {
            WriteServerMessage("# Server wird gestoppt #", MessageStatus.StatusMessage);            
            tcpListener.Stop();            
            Working = false;
            bool status = true;
            while (status)
            {
                try
                {
                    foreach (var item in ConnectedStreams.ToArray())
                    {
                        item.Value.Close();
                    }
                    status = false;
                }
                catch (Exception)
                {
                    status = true;
                }
            }
            while (ConnectedThreads.Count > 0 || ConnectedStreams.Count > 0)
            {
                // wait ...
            }
            WriteServerMessage("# Server wurde gestoppt #",MessageStatus.StatusMessage);            
        }
        #endregion      
        #region enum
        public enum MessageStatus
        {
            HeaderMessage = 0,
            StatusMessage = 1,
            ClientToServer = 2,
            ServerToClient = 3,
        }
        #endregion

    }
}
