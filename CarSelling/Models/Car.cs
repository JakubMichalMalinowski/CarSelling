using System.ComponentModel.DataAnnotations;

namespace CarSelling.Models
{
    public class Car : Vehicle, IEngine
    {
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
