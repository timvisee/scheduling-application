namespace backend.Models
{
    public class EventLocation
    {
        public int EventId { get; set; }
        public Event Event { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
