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

        [Required]
        [Display(Name="Title")]
        public string Title { get; set; }

        [Display(Name="Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name="Start")]
        public DateTime Start { get; set; }

        [Required]
        [Display(Name="End")]
        public DateTime End { get; set; }

        [Display(Name="Owners")]
        public ICollection <EventOwner> Owners { get; set; }

        [Display(Name="Attendees")]
        public ICollection <EventAttendee> Attendees { get; set; }

        [Display(Name="Locations")]
        public ICollection <EventLocation> Locations { get; set; }
    }
}
