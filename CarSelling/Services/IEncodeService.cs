namespace CarSelling.Services
{
    public interface IEncodeService
    {
        public Task<string> EncodeFileAsync(IFormFile file);
    }
}
