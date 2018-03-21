using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Types;

namespace backend.Models
{
    [Table("user")]
    public class User : People
    {
        public string FirstName { get; set; }
        public string Infix { get; set; }
        public string LastName { get; set; }
        public string Locale { get; set; }

        public Type Type { get; set; }
        public Role Role { get; set; }
        public bool Deleted { get; set; }

        /// Full name property
        public string FullName {
            get {
                if(string.IsNullOrEmpty(Infix)) {
                    return FirstName + " " + LastName;
                } else {
                    return FirstName + " " + Infix + " " + LastName;
                }
            }
        }

        // Override abstract getters
        public override string DisplayName {
            get {
                return FullName;
            }
        }
    }
}
