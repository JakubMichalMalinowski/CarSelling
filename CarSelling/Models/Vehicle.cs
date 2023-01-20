using System.ComponentModel.DataAnnotations;

namespace CarSelling.Models
{
    public abstract class Vehicle
    {
        public int Id { get; set; }

        [Required]
        public string Make { get; set; } = string.Empty;

        [Required]
        public string Model { get; set; } = string.Empty;

        [Required]
        [Range(1800, 2200)]
        public ushort ProductionYear { get; set; }
    }
}
