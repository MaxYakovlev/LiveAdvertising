using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveAdvertising.Models.Entities
{
    public class Stream
    {
        public int Id { get; set; }

        public string Source { get; set; }

        public string Products { get; set; }

        public int ShopId { get; set; }

        public Shop Shop { get; set; }

        public List<Message> Messages { get; set; }
    }
}
