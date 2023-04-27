using System.Drawing;
using System.Drawing.Imaging;

namespace CarSelling.Models
{
    public class MimedStream
    {
        public Stream PhotoStream { get; }
        public string? MimeType { get; }

        public MimedStream(Stream photoStream, string? mimeType)
        {
            PhotoStream = photoStream;
            MimeType = mimeType;
        }
    }
}
