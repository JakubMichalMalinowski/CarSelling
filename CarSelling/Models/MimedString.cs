namespace CarSelling.Models
{
    public class MimedString
    {
        public string Type { get; }
        public string Content { get; }
        public MimedString(string type, string content)
        {
            Type = type;
            Content = content;
        }
    }
}
