using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("people")]
    public abstract class People
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Owners")]
        public ICollection <EventOwner> EventsOwn { get; set; }

        [Display(Name="Attendees")]
        public ICollection <EventAttendee> EventsAttend { get; set; }

        [Display(Name="Groups")]
        public ICollection <PeopleGroup> Groups { get; set; }

        // Some generic getters
        [NotMapped]
        [Display(Name = "Name")]
        public abstract string DisplayName { get; }

        [NotMapped]
        [Display(Name = "Type")]
        public abstract string TypedDisplayName { get; }
    }
}
