using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace ChatServer
{
    /// <summary>
    /// Abstracte Klasse von Client
    /// </summary>
    public abstract class Client : IClient
    {
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
            hauptStream.StreamTrennung += new EventHandler(hauptStream_StreamGetrennt);
            ClientStreams.Add(hauptStream);
        }
        #endregion        
        #region Events
        private void hauptStream_StreamGetrennt(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
