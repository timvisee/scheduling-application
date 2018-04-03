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

            // Loop through all people
            foreach(var p in People.Select(p => p.People))
            {
                // TODO: remove after debugging
                System.Console.WriteLine("Processing type: " + p);

                // The people must be new to the sets
                if((p is User && users.Contains(p)) || (p is Group && groups.Contains(p)))
                {
                    // TODO: remove this message after debugging
                    Console.WriteLine("Skipped processing item that has already been collected");
                    continue;
                }

                // Get the users and groups from this people
                p.BuildUserAndGroupSets(ref users, ref groups);
            }
        }
    }
}
