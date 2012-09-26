using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ChatServer
{
    public class HauptServer : Server
    {
        #region Konstruktor
        public HauptServer(IPAddress ipAdresse, int port)
            : base(ipAdresse, port)
        {

        }
        #endregion
        #region Private Funktionen
        internal override void ClientGefunden(TcpClient tcpClient)
        {
            var clientBenutzer = new ClientBenutzer(tcpClient);
        }
        #endregion
    }
}
