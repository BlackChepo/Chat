using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChatServer;
using System.Net;

namespace ChatServerApp
{
    class Program
    {
        

        static void Main(string[] args)
        {
            //var manager = new Manager();
            //manager.Start();

            var Server = new Server(IPAddress.Any, 3000);

        }
    }
}
