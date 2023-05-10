using CarSelling.Models;
using System.Drawing;

namespace CarSelling.Services
{
    public interface IPhotoService
    {
        public IAsyncEnumerable<string> UploadFilesAsync(List<IFormFile> files);
        public Task<MimedStream> DownloadFileAsync(int photoId);
        public Task<MimedString> DownloadEncodedFileAsync(int photoId);
    }
}
