using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ChatServer
{
    public class ClientBenutzer : Client, IBenutzer
    {
        public Benutzer Benutzer { get; private set; }

        public ClientBenutzer(IPAddress ipAdresse, Benutzer benutzer)
            : base(ipAdresse)
        {
            Benutzer = benutzer;
        }
    }
}
