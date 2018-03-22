using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("event")]
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public ICollection <EventOwner> Owners { get; set; }
        public ICollection <EventAttendee> Attendees { get; set; }
        public ICollection <EventLocation> Locations { get; set; }
    }
}
