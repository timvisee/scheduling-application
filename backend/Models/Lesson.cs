using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Lesson : Event
    {
        [Required]
        [Key]
        [ForeignKey("EventId")]
        public int EventId { get; set; }
        public DateTime DTSTAMP { get; set; }
        public string UID { get; set; }
   
   
    }
}
