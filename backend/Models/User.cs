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

        [Display(Name="Remove?")]
        public bool Deleted { get; set; }

        /// Full name property
        public string FullName {
            get
            {
                if(string.IsNullOrEmpty(Infix)) {
                    return FirstName + " " + LastName;
                }

                return FirstName + " " + Infix + " " + LastName;
            }
        }

        // Override abstract getters
        public override string DisplayName => FullName;
        public override string TypedDisplayName => "User: " + FullName;
    }
}
