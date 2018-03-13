using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;
//
namespace backend.Models
{
    [Table("groups")]
    public class Group
    {
        [Key]
        [ForeignKey("People")]
        public int PeopleId { get; set; }
        public string Name { get; set; }

        [ForeignKey("User")]
        public List<User> Users { get; set; }
    }
}
