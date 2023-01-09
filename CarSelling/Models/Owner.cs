using System.ComponentModel.DataAnnotations;

namespace CarSelling.Models
{
    public class Owner
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }

        [Phone]
        public string? PhoneNo { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
    }
}