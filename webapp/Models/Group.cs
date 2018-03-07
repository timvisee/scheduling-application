using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;
//
namespace webapp.Models
{
    [Table("groups")]
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        public string Name { get; set; }
        
        [ForeignKey("People")]
        public int People { get; set; }
        
        [ForeignKey("User")] 
        public List<User> Users { get; set; }
    }
}
