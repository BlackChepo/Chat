using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ChatServer
{
    public class ClientBenutzer : Client, IBenutzer
    {        
        public Benutzer Benutzer { get; private set; }

        public ClientBenutzer(TcpClient tcpClient)
            : base(tcpClient)
        {
            // Checks = ist OK passwort stimmt.
            this.ClientStreams.FirstOrDefault(p => p is HauptClientStream).Sende("OK, Du bist was du bist.");            
        }   
    }
}
