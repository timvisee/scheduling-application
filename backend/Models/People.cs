using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("people")]
    public class People
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PeopleId { get; set; }
    }
}
