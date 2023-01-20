using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CarSelling.Models
{
    public class CarAdResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? PhotoPath { get; set; }
        public decimal Price { get; set; }
        public bool Negotiable { get; set; }

        public string OwnerUserName { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNo { get; set; }
    }
}
