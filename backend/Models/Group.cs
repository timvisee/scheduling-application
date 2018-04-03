using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace backend.Models
{
    [Table("group")]
    public class Group : People
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public ICollection <PeopleGroup> People { get; set; }

        [NotMapped]
        public override string DisplayName => Name;

        [NotMapped]
        public override string TypedDisplayName => "Group: " + Name;

        protected internal override void BuildUserAndGroupSets(
            ref HashSet<User> users,
            ref HashSet<Group> groups
        ) {
            // Make sure people isn't null
            if(People == null)
                throw new Exception("'People' property in group is null, you must eager load it first");

            // Add the current group
            groups.Add(this);

            // Get all people
            var people = People.Select(p => p.People).ToList();
            foreach (var item in people)
                Console.WriteLine("- People type: " + item.GetType().Name);

            // Loop through all people
            foreach(var p in people)
            {
                // The people must be new to the sets
                if(users.Contains(p) || groups.Contains(p))
                    continue;

                // Get the users and groups from this people
                p.BuildUserAndGroupSets(ref users, ref groups);
            }
        }
    }
}
