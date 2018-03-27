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

        public override string DisplayName => Name;

        public override string TypedDisplayName => "Group: " + Name;

        public override List<User> Users => People
            .Select(p => p.People)
            .Select(p => p.Users)
            .Aggregate(new List<User>(), (a, b) => a.Concat(b).ToList())
            .ToList();
    }
}
