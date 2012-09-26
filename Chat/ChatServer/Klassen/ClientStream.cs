using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace ChatServer
{
    public abstract class ClientStream : IClientStream
    {        
        public event EventHandler StreamTrennung;
        public TcpClient tcpClient { get; set; }
        public NetworkStream Stream { get; set; }

        public ClientStream(TcpClient tcpClient)
        {
            if (tcpClient == null)
                throw new ArgumentNullException("Kein Client");

            this.tcpClient = tcpClient;
            this.Stream = tcpClient.GetStream();

            Thread t = new Thread(new ThreadStart(HöreStreamAb));
            t.Start();
        }

        public void SendeZuClient(string nachricht)
        {   
            if (Stream == null)
                return;

            Thread t = new Thread(() =>
            {
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] buffer = encoder.GetBytes(nachricht);

                Stream.Write(buffer, 0, buffer.Length);
                Stream.Flush();
            });
            t.Start();
        }

        internal virtual void HöreStreamAb()
        {
            throw new NotImplementedException("Höre Stream Ab nicht implementiert.");
        }

        internal void StreamGetrennt()
        {
            if (StreamTrennung != null)
                StreamTrennung(this, null);
        }
    }
}
