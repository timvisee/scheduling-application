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

        [Display(Name="Titel")]
        public ICollection <EventOwner> EventsOwn { get; set; }

        [Display(Name="Titel")]
        public ICollection <EventAttendee> EventsAttend { get; set; }

        [Display(Name="Groepen")]
        public ICollection <PeopleGroup> Groups { get; set; }

        // Some generic getters
        [Display(Name = "Naam")]
        public abstract string DisplayName { get; }

        [Display(Name = "Type")]
        public abstract string TypedDisplayName { get; }
    }
}
