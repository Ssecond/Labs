using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace Practica18102023
{
    internal interface LogException
    {
        internal void LogXML(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LogException));

            using (FileStream fs = new FileStream("log.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, this);
            }
        }
        internal void LogJSON(string path)
        {
            using (FileStream fs = new FileStream("log.json", FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, this);
            }
        }
        internal void LogTXT(string path)
        {
#error Не реализовано логгирование в тхт
        }
    }
}
