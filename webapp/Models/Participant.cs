using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp.Models
{
    [Table("participants")]
    public class Participant
    {
        [Key] 
        public int ParticipantId { get; set; }
    }
}