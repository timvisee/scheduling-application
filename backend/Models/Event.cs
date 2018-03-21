using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("events")]
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        [ForeignKey("People")]
        public List<People> Peoples { get; set; }

        public ICollection <EventLocation> Locations { get; set; }
    }
}
