using CarSelling.Models;

namespace CarSelling.Services
{
    public interface ICarAdService
    {
        public IEnumerable<CarAd> GetAll();
        public Task<CarAd?> GetByIdAsync(int id);
        public Task CreateAsync(CarAd carAd);
        public Task UpdateAsync(int id, CarAd carAd);
        public Task DeleteAsync(int id);
    }
}
