using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp.Models
{
    [Table("participants")]
    public class Participant
    {
        [Key] private int Id { get; set; }
    }
}