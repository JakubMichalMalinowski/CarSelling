using CarSelling.Exceptions;

namespace CarSelling.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IConfiguration _configuration;

        public PhotoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async IAsyncEnumerable<string> UploadFilesAsync(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(
                        _configuration["Storage:Path"] ?? throw new NullFieldException(),
                        Path.GetRandomFileName());
                    using (var stream = File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }

                    yield return filePath;
                }
            }
        }
    }
}
