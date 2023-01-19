using System.ComponentModel.DataAnnotations;

namespace CarSelling.Models
{
    public class UserRequestDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Phone]
        public string? PhoneNo { get; set; }

    }
}
