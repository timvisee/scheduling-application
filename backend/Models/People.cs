using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("people")]
    public abstract class People
    {
        [Key]
        public int Id { get; set; }

        public ICollection <PeopleGroup> Groups { get; set; }

        // Some generic getters
        public abstract string DisplayName { get; }
    }
}
