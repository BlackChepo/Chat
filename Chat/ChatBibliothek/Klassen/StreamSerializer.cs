using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace ChatBibliothek
{
    public class StreamSerializer <T>
    {
        public string Serialize(T objekt)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, objekt);

                return writer.ToString();
            }
        }        

        public T Deserialize(string xml)
        {            
            object objekt = null;

            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                using (TextReader tr = new StringReader(xml))
                {
                    objekt = ser.Deserialize(tr);
                }
            }
            catch (InvalidOperationException)
            {                
                throw new ArgumentException("Falsches Objekt");
            }

            if (objekt is T)
            {
                return (T)objekt;
            }
            else
            {
                try
                {
                    return (T)Convert.ChangeType(objekt, typeof(T));
                }
                catch (InvalidCastException)
                {
                    return default(T);
                }
            }            
        }
    }
}
