using System.ComponentModel.DataAnnotations.Schema;

namespace LiveAdvertising.Models.Entities
{
    public class Message
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Time { get; set; }

        [Column(TypeName = "boolean")]
        public bool isAnswer { get; set; }

        public int StreamId { get; set; }

        public Stream Stream { get; set; }
    }
}
