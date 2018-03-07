using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;

namespace webapp.Models
{
    [Table("events")]
    public class Event
    {
        [Key] 
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        // change to groepen > groepen links to people

        [ForeignKey("People")]
        public List<People> Peoples { get; set; }
    }
}
