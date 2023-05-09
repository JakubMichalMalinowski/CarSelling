namespace CarSelling.Models
{
    public class EncodedFile : MimedString
    {
        public int Id { get; set; }

        public EncodedFile(string type, string content) : base(type, content) { }
    }
}
