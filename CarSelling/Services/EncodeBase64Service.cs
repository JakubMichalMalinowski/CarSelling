using CarSelling.Models;

namespace CarSelling.Services
{
    public class EncodeBase64Service : IEncodeService
    {
        public IList<MimedString> EncodeFiles(List<IFormFile> files)
        {
            List<MimedString> encodedFiles = new();
            files.ForEach(file =>
            {
                MemoryStream stream = new();

                file.CopyTo(stream);

                string encodedFile = Convert
                .ToBase64String(stream.ToArray());

                stream.Dispose();

                encodedFiles.Add(
                    new MimedString(file.ContentType, encodedFile));
            });

            return encodedFiles;
        }

        public byte[] DecodeFile(string encodedFile)
        {
            return Convert.FromBase64String(encodedFile);
        }
    }
}
