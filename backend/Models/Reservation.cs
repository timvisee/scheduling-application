namespace backend.Models
{
    [Table("reservation")]
    public class Reservation

        {
            [Key]
            public int Id { get; set; }
            public string Description { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }

            [ForeignKey("People")]
            public List<People> Peoples { get; set; }

            public ICollection <EventLocation> Locations { get; set; }
        }
    }
}
