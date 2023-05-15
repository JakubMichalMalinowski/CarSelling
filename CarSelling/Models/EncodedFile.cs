using System.Text.Json.Serialization;

namespace CarSelling.Models
{
    public class EncodedFile : MimedString
    {
        [JsonIgnore]
        public int Id { get; set; }

        public EncodedFile(string type, string content) : base(type, content) { }
    }
}
