using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace ChatServer
{
    public interface IClientStream
    {
        event EventHandler StreamTrennung;

        TcpClient tcpClient { get; set; }
        NetworkStream Stream { get; set; }

        void SendeZuClient(string nachricht);
    }
}
