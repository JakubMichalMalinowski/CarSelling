namespace CarSelling.Models
{
    public class CarAdSimpleResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool Negotiable { get; set; }
        public int? MainPhotoId { get; set; }
        public PhotoLocation? MainPhotoLocation { get; set; }
    }
}
