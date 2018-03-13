using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("groups")]
    public class Group : People
    {
        [Key]
        [ForeignKey("People")]
        public int PeopleId { get; set; }

        public string Name { get; set; }

        [ForeignKey("People")]
        public List<People> Peoples { get; set; }
    }
}
