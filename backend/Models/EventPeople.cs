using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("event_people")]
    public class EventPeople
    {
        public int EventId { get; set; }
        public Event Event { get; set; }

        public int PeopleId { get; set; }
        public People People { get; set; }
    }
}
