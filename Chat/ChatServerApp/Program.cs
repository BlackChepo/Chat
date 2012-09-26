using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChatServer;
using System.Net;
using System.Xml.Serialization;
using System.IO;

namespace ChatServerApp
{
    class Program
    {
        

        static void Main(string[] args)
        {
            //var manager = new Manager();
            //manager.Start();

            //WriteXML();
            //return;

            Manager manager = new Manager();
            manager.Start();
        }

        

        public static void WriteXML()
        {
            Book overview = new Book();
            overview.title = "Serialization Overview";

            SerializeToString(overview);

            Console.ReadLine();
        }


        public static void SerializeToString(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                
                Console.WriteLine(writer.ToString());
            }
        }
    }

    public class Book
    {
        public String title;

    }
}
