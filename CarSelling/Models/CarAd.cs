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
        public decimal Price { get; set; }

        [Required]
        public bool Negotiable { get; set; }

        [Required]
        public Owner Owner { get; set; } = default!;
    }
}