using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using ChatBibliothek;

namespace ChatServer
{
    /// <summary>
    /// Abstracte Klasse von Client
    /// </summary>
    public abstract class Client : IClient
    {
        #region Variablen
        private bool TrennungsVorgang = false;
        public delegate void ClientHandler(IClient client);
        public event ClientHandler VerbindungGetrennt;
        object sperre = new Object();
        #endregion
        #region Eigenschaften
        /// <summary>
        /// IP Adresse des Clients
        /// </summary>
        public IPAddress IPAdresse { get; set; }
        public Guid GUID { get; set; }
        public List<IClientStream> ClientStreams { get; set; }
        #endregion
        #region Konstruktor
        public Client(TcpClient tcpClient)
        {          
            if (tcpClient == null)
                throw new ArgumentNullException("Kein Client");

            GUID = Guid.NewGuid();
            IPAdresse = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address;
            ClientStreams = new List<IClientStream>();
            
            var hauptStream = new HauptClientStream(tcpClient);
            hauptStream.StreamTrennung += new EventHandler(StreamGetrenntEvent);
            ClientStreams.Add(hauptStream);
        }
        #endregion        
        #region Events
        private void StreamGetrenntEvent(object sender, EventArgs e)
        {
            lock (sperre)
            {
                if (TrennungsVorgang)
                return;

                TrennungsVorgang = true;
                ClientStreams.ForEach(p => p.StreamTrennung -= StreamGetrenntEvent);
                ClientStreams.ForEach(p => p.Stream.Close());

                if (VerbindungGetrennt != null)
                    VerbindungGetrennt(this);
            }
        }
        #endregion
    }
}
