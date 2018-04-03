using System;
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
        public ICollection<EventOwner> EventsOwn { get; set; }

        [Display(Name="Attendees")]
        public ICollection<EventAttendee> EventsAttend { get; set; }

        [Display(Name="Groups")]
        public ICollection<PeopleGroup> Groups { get; set; }

        // Some generic getters
        [NotMapped]
        [Display(Name = "Name")]
        public abstract string DisplayName { get; }

        [NotMapped]
        [Display(Name = "Type")]
        public abstract string TypedDisplayName { get; }

        /// <summary>
        /// This method fetches a list of all users and groups that are part of
        /// this people.
        /// This method fetches everything in a recursive manner.
        /// No duplicates are returned.
        /// </summary>
        protected internal abstract void BuildUserAndGroupSets(ref HashSet<User> users, ref HashSet<Group> groups);

        /// <summary>
        /// Get a set of users that are part of this people.
        /// If this instance is a user, a list with a single user will be
        /// returned.
        /// If this instance is a group, a list with all the users that are part
        /// of the groups is returned. It is possible the list is empty.
        /// This method fetches all users from groups in a recursive manner.
        /// The list doesn't contains duplicates.
        /// </summary>
        [Display(Name = "Has users (test, deep)")]
        public HashSet<User> AllUsers() {
            // Create a user and group set
            HashSet<User> users = new HashSet<User>();
            HashSet<Group> groups = new HashSet<Group>();

            // Fetch the users and groups
            BuildUserAndGroupSets(ref users, ref groups);

            // Remove itself
            if(this is User)
                users.Remove((User) this);

            // TODO: remove after debugging
            Console.WriteLine("Got user count: " + users.Count);

            // Return the users
            return users;
        }

        /// <summary>
        /// Get a set of groups that are part of this people.
        /// If this instance is a user, an empty list is returned.
        /// This method fetches all groups from parent groups in a recursive manner.
        /// The list doesn't contains duplicates.
        /// </summary>
        [Display(Name = "Has groups (test, deep)")]
        public HashSet<Group> AllGroups() {
            // Create a user and group set
            HashSet<User> users = new HashSet<User>();
            HashSet<Group> groups = new HashSet<Group>();

            // Fetch the users and groups
            BuildUserAndGroupSets(ref users, ref groups);

            // TODO: remove after debugging
            Console.WriteLine("Got group count: " + groups.Count);

            // Remove itself
            if(this is Group)
                groups.Remove((Group) this);

            // Return the users
            return groups;
        }
    }
}
