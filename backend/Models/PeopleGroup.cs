using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("people_group")]
    public class PeopleGroup
    {
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        public int PeopleId { get; set; }
        public virtual People People { get; set; }
    }
}
