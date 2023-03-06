namespace CarSelling.Services
{
    public interface IPhotoService
    {
        public IAsyncEnumerable<string> UploadFilesAsync(List<IFormFile> files);
    }
}
