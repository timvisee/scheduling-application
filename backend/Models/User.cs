using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapp.Types;

namespace webapp.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        
        [ForeignKey("People")]
        public int People { get; set; }
        
        public string FirstName { get; set; }
        public string Infix { get; set; }
        public string LastName { get; set; }
        public string Locale { get; set; }

        public Type Type { get; set; }
        public Role Role { get; set; }
        public bool Deleted { get; set; }

        
    }
}
