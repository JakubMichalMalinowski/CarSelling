using CarSelling.Models;

namespace CarSelling.Services
{
    public interface ICarAdService
    {
        public Task<IEnumerable<CarAdDto>> GetAllCarAdsAsync();
        public Task<CarAdDto?> GetCarAdByIdAsync(int id);
        public Task CreateCarAdAsync(CarAdDto carAd);
        public Task UpdateCarAdAsync(int id, CarAdDto carAd);
        public Task DeleteCarAdAsync(int id);
    }
}
