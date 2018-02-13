using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp.Models
{
    [Table("locations")]
    public class Location
    {
        [Key]
        private int Id { get; set; }
        private string Name { get; set; }
        private string Description { get; set; }
        private double Latitude { get; set; }
        private double Longitude { get; set; }
    }
}