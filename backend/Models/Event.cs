using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapp.Models;

namespace backend.Models
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

        [ForeignKey("People")]
        public List<People> Peoples { get; set; }

        public ICollection <EventLocation> EventLocations { get; set; }
    }
}
