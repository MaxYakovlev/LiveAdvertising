using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace LiveAdvertising.Parsing
{
    [Serializable]
    [XmlRoot("yml_catalog")]
    public class Catalog
    {
        [XmlAttribute("date")]
        public string Date { get; set; }
        [XmlElement("shop")]
        public Shop[] Shops { get; set; }

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static Catalog Deserialize(string content)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Catalog));
            Catalog catalog;

            using (TextReader reader = new StringReader(content))
            {
                catalog = (Catalog)serializer.Deserialize(reader);
            }

            return catalog;
        }
    }
}
