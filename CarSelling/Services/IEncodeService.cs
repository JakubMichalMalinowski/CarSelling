using CarSelling.Models;

namespace CarSelling.Services
{
    public interface IEncodeService
    {
        public IList<MimedString> EncodeFiles(List<IFormFile> files);
        public byte[] DecodeFile(string encodedFile);
    }
}
