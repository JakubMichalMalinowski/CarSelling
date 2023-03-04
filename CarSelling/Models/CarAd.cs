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

        [Required]
        [Precision(18,2)]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public bool Negotiable { get; set; }

        [Required]
        public Car Car { get; set; } = default!;

        [Required]
        public User CreatedBy { get; set; } = default!;

        public List<FilePath> PhotoPaths { get; set; } = new List<FilePath>();
    }
}