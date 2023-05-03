namespace CarSelling.Services
{
    public class EncodeBase64Service : IEncodeService
    {
        public async Task<string> EncodeFileAsync(IFormFile file)
        {
            MemoryStream stream = new();
            await file.CopyToAsync(stream);
            string encodedFile = Convert
                .ToBase64String(stream.ToArray());
            stream.Dispose();
            return encodedFile;
        }
    }
}
