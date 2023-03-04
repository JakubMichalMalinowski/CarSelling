using System.ComponentModel.DataAnnotations;

namespace CarSelling.Models
{
    public class FilePath
    {
        public int Id { get; set; }

        [Required]
        public string Path { get; set; } = string.Empty;
    }
}
