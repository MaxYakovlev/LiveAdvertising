using System;
using System.Xml.Serialization;

namespace LiveAdvertising.Parsing
{
    [Serializable]
    public class Shop
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("company")]
        public string Url { get; set; }
        [XmlElement("url")]
        public string Image { get; set; }
        [XmlArray("offers")]
        [XmlArrayItem("offer", typeof(Offer))]
        public Offer[] Offers { get; set; }
    }
}
