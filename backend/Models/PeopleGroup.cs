using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("people_group")]
    public class PeopleGroup
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int PeopleId { get; set; }
        public People People { get; set; }
    }
}
