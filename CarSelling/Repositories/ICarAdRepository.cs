using CarSelling.Models;

namespace CarSelling.Repositories
{
    public interface ICarAdRepository
    {
        public IQueryable<CarAd> GetAllCarAds();
        public Task<CarAd?> GetCarAdByIdAsync(int id);
        public Task CreateCarAdAsync(CarAd carAd);
        public Task UpdateCarAdAsync(CarAd carAd);
        public Task DeleteCarAdAsync(CarAd carAd);
        public Task<bool> AdExistsAsync(int id);
    }
}
