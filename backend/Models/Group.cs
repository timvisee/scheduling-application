using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("groups")]
    public class Group : People
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("People")]
        public int PeopleId { get; set; }

        public string Name { get; set; }

        public ICollection <UserGroup> Users { get; set; }
    }
}
