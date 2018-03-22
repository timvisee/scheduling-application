using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("group")]
    public class Group : People
    {
        [Display(Name = "Naam")]
        public string Name { get; set; }

        public ICollection <PeopleGroup> Peoples { get; set; }

        public override string DisplayName => Name;
        public override string TypedDisplayName => "Group: " + Name;
    }
}
