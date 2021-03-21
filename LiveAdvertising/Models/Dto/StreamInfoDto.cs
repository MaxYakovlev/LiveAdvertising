using LiveAdvertising.Models.Entities;
using LiveAdvertising.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveAdvertising.Models.Dto
{
    public class StreamInfoDto
    {
        public string Source { private get; set; }

        public string GetSourceId()
            => Source.Split("?v=")[1];

        public string Products { private get; set; }

        public Catalog GetCatalog() => Catalog.Deserialize(Products);

        public string ShopName { get; set; }

        public List<Message> Messages { get; set; }
    }
}
