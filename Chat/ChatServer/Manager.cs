using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Net;
using ChatBibliothek;

namespace ChatServer
{
    public sealed class Manager
    {
        #region Static Config
        public static readonly int HauptServerPort = 3000;
        public static readonly int ChatServerPort = 3001;
        public static readonly int FileServerPort = 3002;
        #endregion
        #region Variablen
        private HauptServer HauptServer;        
        #endregion
        #region Eigenschaften
        public int Plätze { get; private set; }
        /// <summary>
        /// Gibt an, ob der Server läuft.        
        /// </summary>
        [DefaultValue(false)]
        public bool ServerArbeitsStatus { get; private set; }
        public List<IClient> Clients { get; private set; }
        #endregion
        #region Konstruktor
        public Manager(int plätze = 20)
        {
            this.Plätze = plätze;
        }
        #endregion
        #region Öffentliche Funktionen
        public void Start()
        {
            ServerArbeitsStatus = true;
            StarteServer();            
        }        
        public void Stop()
        {
            ServerArbeitsStatus = false;            
        }        
        #endregion
        #region Private Funktionen
        private void StarteServer()
        {
            if (HauptServer == null)
                HauptServer = new HauptServer(IPAddress.Any, HauptServerPort);         
        }        
        #endregion        
    }
}
