using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace backend.Models
{
    [Table("group")]
    public class Group : People
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public virtual ICollection<PeopleGroup> People { get; set; }

        [NotMapped]
        public override string DisplayName => Name;

        [NotMapped]
        public override string TypedDisplayName => "Group: " + Name;

        public override bool IsUser() {
            return false;
        }

        public override bool IsGroup() {
            return true;
        }

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
            foreach (var p in People.Select(p => p.People))
            {
                // The people must be new to the sets
                if(users.Contains(p) || groups.Contains(p))
                    continue;

                // Get the users and groups from this people
                p.BuildUserAndGroupSets(ref users, ref groups);
            }
        }
    }

    public class GroupView
    {
        public Group group { get; set; }

        public List<People> People { get; set; }

        [Display(Name = "People")]
        public IEnumerable<SelectListItem> PeopleList { get; set; }

        public IEnumerable<int> SelectedPeople { get; set; }
    }
}
