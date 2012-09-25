using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Net;

namespace ChatServer
{
    public class Manager
    {
        #region Variablen
        internal DateTime _ServerStart;        
        #endregion
        #region Eigenschaften
        public int Plätze { get; private set; }
        /// <summary>
        /// Gibt an, ob der Server läuft.        
        /// </summary>
        [DefaultValue(false)]
        public bool ServerArbeitsStatus { get; internal set; }
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
            OnStart();
        }        
        public void Stop()
        {
            ServerArbeitsStatus = false;
            OnStop();
        }        
        #endregion
        #region Private Funktionen
        internal virtual void OnStop()
        {

        }
        internal virtual void OnStart()
        {
            var server = new Server(IPAddress.Any,3000);
        }
        #endregion        
    }
}
