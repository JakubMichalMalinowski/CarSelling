namespace CarSelling.Services
{
    public interface IEncodeService
    {
        public IList<string> EncodeFiles(List<IFormFile> files);
    }
}
