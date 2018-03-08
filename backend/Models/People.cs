using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp.Models
{
    [Table("people")]
    public class People
    {
        [Key] 
        public int PeopleId { get; set; }
    }
}