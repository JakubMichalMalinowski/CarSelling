using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CarSelling.Models
{
    public class CarAd
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? PhotoPath { get; set; }

        [Required]
        [Precision(18,2)]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public bool Negotiable { get; set; }

        [Required]
        public Owner Owner { get; set; } = default!;
    }
}