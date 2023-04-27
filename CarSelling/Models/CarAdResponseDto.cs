using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CarSelling.Models
{
    public class CarAdResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool Negotiable { get; set; }

        public string OwnerUserName { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNo { get; set; }

        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public ushort ProductionYear { get; set; }

        public Body Body { get; set; }
        public FuelType FuelType { get; set; }
        public double EngineCapacity { get; set; }
        public ushort Power { get; set; }
        public uint Mileage { get; set; }
        public Drivetrain Drivetrain { get; set; }
        public Transmission Transmission { get; set; }
        public int[]? PhotoIds { get; set; }
    }
}
