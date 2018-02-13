using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;

namespace webapp.Models
{
    public class Event
    {
        [Key] 
        private int Id { get; set; }
        private string Title { get; set; }
        private string Description { get; set; }
        private DateTime DateStart { get; set; }
        private DateTime DateEnd { get; set; }
        [ForeignKey("Participant")]
        private ArrayList<Participant> Participants { get; set; }

        
    }
}