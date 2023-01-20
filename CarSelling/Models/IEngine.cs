namespace CarSelling.Models
{
    public interface IEngine
    {
        public FuelType FuelType { get; set; }
        public ushort EngineCapacity { get; set; }
        public ushort Power { get; set; }
    }
}
