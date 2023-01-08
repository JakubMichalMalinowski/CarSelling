using CarSelling.Models;

namespace CarSelling.Services
{
    public interface ICarAdService
    {
        public IEnumerable<CarAd> GetAll();
        public Task<CarAd?> GetByIdAsync(int id);
        public Task CreateCarAdAsync(CarAd carAd);
    }
}
