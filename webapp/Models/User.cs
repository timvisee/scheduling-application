using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp.Models
{
    [Table("users")]
    public class User : Participant
    {
        [Key] 
        private int Id { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string Locale { get; set; }
        private int Type { get; set; }
        private int Role { get; set; }
        private bool Deleted { get; set; }

        
    }
}