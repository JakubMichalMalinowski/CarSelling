using System.ComponentModel.DataAnnotations;

namespace CarSelling.Models
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Phone]
        public string? PhoneNo { get; set; }

    }
}
