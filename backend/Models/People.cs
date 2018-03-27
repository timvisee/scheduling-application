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

        /// <summary>
        /// Get a list of all users that are part of this people instance.
        /// If this instance is a user, a list with a single user is returned.
        /// If this instance is a group, a list of all users in this group is
        /// returned. People are indexed recursively, the result list may be
        /// empty.
        /// </summary>
        [NotMapped]
        public abstract List<User> Users { get; }
    }
}
