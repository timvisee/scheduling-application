using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Types;

namespace backend.Models
{
    [Table("user")]
    public class User : People
    {
        [Required]
        [Display(Name="First name")]
        public string FirstName { get; set; }

        [Display(Name="Infix")]
        public string Infix { get; set; }

        [Required]
        [Display(Name="Last name")]
        public string LastName { get; set; }

        [Display(Name="Locale")]
        public string Locale { get; set; }

        [Display(Name="Type")]
        public Type Type { get; set; }

        [Display(Name="Role")]
        public Role Role { get; set; }

        [Display(Name="Deleted")]
        public bool Deleted { get; set; }

        public override bool IsUser() {
            return true;
        }

        public override bool IsGroup() {
            return false;
        }

        /// Full name property
        [NotMapped]
        public string FullName {
            get
            {
                if(string.IsNullOrEmpty(Infix)) {
                    return FirstName + " " + LastName;
                }

                return FirstName + " " + Infix + " " + LastName;
            }
        }

        [NotMapped]
        public override string DisplayName => FullName;

        [NotMapped]
        public override string TypedDisplayName => "User: " + FullName;

        protected internal override void BuildUserAndGroupSets(ref HashSet<User> users, ref HashSet<Group> groups) {
            users.Add(this);
        }
    }
}
