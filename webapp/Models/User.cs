using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapp.Types;

namespace webapp.Models
{
    [Table("users")]
    public class User : Participant
    {
        [Key] 
        private int Id { get; set; }
        private string FirstName { get; set; }
        private string Infix { get; set; }
        private string LastName { get; set; }
        private string Locale { get; set; }
        private Type Type { get; set; }
        private Role Role { get; set; }
        private bool Deleted { get; set; }

        
    }
}