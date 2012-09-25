using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatServer
{
    public partial class ChatEntities
    {
        /// <summary>
        /// Aktion im neuem Kontext
        /// </summary>
        /// <param name="aktion">aktion</param>
        public static void AktionInNeuemKontext(Action<ChatEntities> aktion)
        {
            if (aktion == null)
                throw new ArgumentNullException("aktion");

            using (var kontext = new ChatEntities())
            {
                aktion(kontext);
            }
        }
    }
}
