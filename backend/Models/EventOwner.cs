using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("event_owner")]
    public class EventOwner
    {
        public int EventId { get; set; }
        public Event Event { get; set; }

        public int PeopleId { get; set; }
        public People People { get; set; }
    }
}
