using System;
using System.Xml.Serialization;

namespace LiveAdvertising.Parsing
{
    [Serializable]
    public class Param
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlText]
        public string Value { get; set; }
    }
}
