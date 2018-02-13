using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;

namespace webapp.Models
{
    [Table("groups")]
    public class Group : Participant
    {
        [Key] private int Id { get; set; }
        private string Name { get; set; }
        [ForeignKey("User")] private ArrayList<User> Users { get; set; }
    }
}