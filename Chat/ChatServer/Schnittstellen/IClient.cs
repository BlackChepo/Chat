using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ChatServer
{
    public interface IClient
    {
        IPAddress IPAdresse { get; set; }
        Guid GUID { get; set; }
    }
}
