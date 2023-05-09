namespace CarSelling.Models
{
    public class MimedString
    {
        public string Type { get; private set; }
        public string Content { get; private set; }

        public MimedString(string type, string content)
        {
            Type = type;
            Content = content;
        }
    }
}
