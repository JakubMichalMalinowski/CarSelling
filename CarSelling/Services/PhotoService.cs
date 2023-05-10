using CarSelling.Exceptions;
using CarSelling.Models;
using CarSelling.Repositories;
using Microsoft.AspNetCore.StaticFiles;

namespace CarSelling.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IConfiguration _configuration;
        private readonly IPhotoRepository<FilePath> _filePathRepository;
        private readonly IPhotoRepository<EncodedFile> _encodedFileRepository;

        public PhotoService(IConfiguration configuration, IPhotoRepository<FilePath> filePathRepository,
            IPhotoRepository<EncodedFile> encodedFileRepository)
        {
            _configuration = configuration;
            _filePathRepository = filePathRepository;
            _encodedFileRepository = encodedFileRepository;
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

        public async Task<MimedStream> DownloadFileAsync(int photoId)
        {
            var filePath = await _filePathRepository.GetPhotoByIdAsync(photoId);

            var stream = File.OpenRead(filePath?.Path
                ?? throw new NotFoundException());

            new FileExtensionContentTypeProvider().TryGetContentType(filePath.Path, out var type);

            return new MimedStream(stream, type);
        }

        public async Task<MimedString> DownloadEncodedFileAsync(int photoId)
        {
            return await _encodedFileRepository.GetPhotoByIdAsync(photoId) ?? throw new NotFoundException();
        }
    }
}
