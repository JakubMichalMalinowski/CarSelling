using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CarSelling.Models
{
    public class CarAdDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }

        public decimal Price { get; set; }

        public bool Negotiable { get; set; }

        public Car Car { get; set; } = default!;

        public User CreatedBy { get; set; } = default!;

        public int[]? PhotoPathIds { get; set; }

        public int[]? EncodedPhotoIds { get; set; }
    }
}
