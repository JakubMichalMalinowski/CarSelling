using System.ComponentModel.DataAnnotations;

namespace CarSelling.Models
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string HashedPassword { get; set; } = string.Empty;
    }
}
