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

        [Display(Name="Titel")]
        public string Title { get; set; }

        [Display(Name="Beschrijving")]
        public string Description { get; set; }

        [Display(Name="Begin")]
        public DateTime Start { get; set; }

        [Display(Name="Eind")]
        public DateTime End { get; set; }

        [Display(Name="Eigenaren")]
        public ICollection <EventOwner> Owners { get; set; }

        [Display(Name="Deelnemers")]
        public ICollection <EventAttendee> Attendees { get; set; }

        [Display(Name="Locaties")]
        public ICollection <EventLocation> Locations { get; set; }
    }
}
