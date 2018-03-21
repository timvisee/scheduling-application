using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("group")]
    public class Group : People
    {
        public string Name { get; set; }

        public ICollection <PeopleGroup> Peoples { get; set; }
    }
}
