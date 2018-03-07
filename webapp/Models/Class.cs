using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Class
    {        
        [Key]
        public int ClassID { get; set; }
        public string Summary { get; set; }
        public string Location { get; set; }
        public DateTime DTSTAMP { get; set; }
        public DateTime DTSTART { get; set; }
        public DateTime DTEND { get; set; }
        public string UID { get; set; }
   
   
    }
}
