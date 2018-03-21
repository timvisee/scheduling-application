using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("event_location")]
    public class EventLocation
    {
        public int EventId { get; set; }
        public Event Event { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
