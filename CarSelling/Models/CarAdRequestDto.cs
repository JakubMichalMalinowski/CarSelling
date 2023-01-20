using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CarSelling.Models
{
    public class CarAdRequestDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? PhotoPath { get; set; }

        [Required]
        [Precision(18, 2)]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public bool Negotiable { get; set; }

        [Required]
        public string Make { get; set; } = string.Empty;

        [Required]
        public string Model { get; set; } = string.Empty;

        [Required]
        [Range(1800, 2200)]
        public ushort ProductionYear { get; set; }


        [Required]
        public Body Body { get; set; }

        [Required]
        public FuelType FuelType { get; set; }

        [Required]
        [Range(0, 30)]
        public double EngineCapacity { get; set; }

        [Required]
        [Range(0, 10000)]
        public ushort Power { get; set; }

        [Required]
        public uint Mileage { get; set; }

        [Required]
        public Drivetrain Drivetrain { get; set; }

        [Required]
        public Transmission Transmission { get; set; }

    }
}
