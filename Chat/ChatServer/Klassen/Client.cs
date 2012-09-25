using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;

namespace ChatServer
{
    /// <summary>
    /// Abstracte Klasse von Client
    /// </summary>
    public abstract class Client : IClient
    {
        /// <summary>
        /// IP Adresse des Clients
        /// </summary>
        public IPAddress IPAdresse { get; set; }
        public Guid GUID { get; set; }

        public Client(IPAddress ipAdresse)
        {
            IPAdresse = IPAdresse;
        }
    }
}
