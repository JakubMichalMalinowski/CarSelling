using System.Xml.Serialization;

namespace CarSelling.Models
{
    public class MimedString
    {
        public string Type { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public MimedString(string type, string content)
        {
            Type = type;
            Content = content;
        }

        public MimedString() { }
    }
}
