using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
