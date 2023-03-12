using CarSelling.Exceptions;
using CarSelling.Repositories;
using System.Drawing;

namespace CarSelling.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IConfiguration _configuration;
        private readonly ICarAdRepository _carAdRepository;

        public PhotoService(IConfiguration configuration, ICarAdRepository carAdRepository)
        {
            _configuration = configuration;
            _carAdRepository = carAdRepository;
        }

        public async IAsyncEnumerable<string> UploadFilesAsync(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileExt = Path.GetExtension(file.FileName);
                    string filePath;
                    do
                    {
                        filePath = Path.Combine(
                            _configuration["Storage:Path"] ?? throw new NullFieldException(),
                            Path.ChangeExtension(Path.GetRandomFileName(), fileExt));
                    }
                    while (File.Exists(filePath));
                    using (var stream = File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }

                    yield return filePath;
                }
            }
        }

        public Image DownloadFile(int photoId)
        {
            throw new NotImplementedException();
        }
    }
}
