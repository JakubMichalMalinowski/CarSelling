using System.ComponentModel.DataAnnotations;

namespace CarSelling.Models
{
    public class UserCreationDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

    }
}
