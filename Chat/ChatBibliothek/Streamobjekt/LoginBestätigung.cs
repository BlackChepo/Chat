using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ChatBibliothek
{
    public class Bestätigung : IStreamObjekt
    {
        public IPAddress HauptServer { get; set; }
        public int HauptPort { get; set; }

        public IPAddress ChatServer { get; set; }
        public int FilePort { get; set; }

        public IPAddress FileServer { get; set; }
        public int FilePort { get; set; }

        public Guid GUID { get; set; }
    }
}
