using System;
using System.Xml.Serialization;

namespace LiveAdvertising.Parsing
{
    [Serializable]
    public class Offer
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlAttribute("parent")]
        public string Parent { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("url")]
        public string Url { get; set; }
        [XmlElement("price")]
        public string Price { get; set; }
        [XmlElement("currencyId")]
        public string CurrencyId { get; set; }
        [XmlElement("image")]
        public string Image { get; set; }
        [XmlElement("param")]
        public Param[] Params { get; set; }
    }
}
