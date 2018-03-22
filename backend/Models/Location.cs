using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("locations")]
    public class Location
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Naam")]
        public string Name { get; set; }

        [Display(Name="Beschrijving")]
        public string Description { get; set; }

        [Display(Name="Latitude")]
        public double Latitude { get; set; }

        [Display(Name="Longitude")]
        public double Longitude { get; set; }

        public ICollection <EventLocation> Events { get; set; }
    }
}
