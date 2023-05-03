namespace CarSelling.Services
{
    public class EncodeBase64Service : IEncodeService
    {
        public IList<string> EncodeFiles(List<IFormFile> files)
        {
            List<string> encodedFiles = new();
            files.ForEach(file =>
            {
                MemoryStream stream = new();

                file.CopyTo(stream);

                string encodedFile = Convert
                .ToBase64String(stream.ToArray());

                stream.Dispose();

                encodedFiles.Add(encodedFile);
            });

            return encodedFiles;
        }
    }
}
