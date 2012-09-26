using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace ChatServer
{
    public class HauptClientStream : ClientStream
    {
        public HauptClientStream(TcpClient tcpClient)
            : base(tcpClient)
        {
            
        }

        internal override void HöreStreamAb()
        {
            byte[] message = new byte[4096];
            int bytesRead;

            ASCIIEncoding encoder = new ASCIIEncoding();
            while (true)
            {
                bytesRead = 0;

                try
                {
                    bytesRead = Stream.Read(message, 0, 4096);
                }
                catch (System.IO.IOException)
                {
                    break;
                    // Kein Verbindung mehr zum Stream;
                }

                if (bytesRead == 0)
                    break;

                Console.WriteLine(encoder.GetString(message, 0, bytesRead));
            }

            StreamGetrennt();
        }
    }
}
