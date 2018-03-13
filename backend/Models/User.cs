using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Types;

namespace backend.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [ForeignKey("People")]
        public int PeopleId { get; set; }

        public string FirstName { get; set; }
        public string Infix { get; set; }
        public string LastName { get; set; }
        public string Locale { get; set; }

        public Type Type { get; set; }
        public Role Role { get; set; }
        public bool Deleted { get; set; }


    }
}
