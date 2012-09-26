using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatBibliothek
{
    public class LoginAnfrage : IStreamObjekt
    {
        public string Name { get; set; }
        public string Passwort { get; set; }
    }
}
