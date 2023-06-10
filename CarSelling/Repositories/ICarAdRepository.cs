using CarSelling.Models;

namespace CarSelling.Repositories
{
    public interface ICarAdRepository
    {
        public Task<IEnumerable<CarAdDto>> GetAllCarAdsAsync();
        public Task<CarAdDto?> GetCarAdByIdAsync(int id);
        public Task<CarAd?> GetCarAdAsync(int id);
        public Task CreateCarAdAsync(CarAd carAd);
        public Task UpdateCarAdAsync(CarAd carAd);
        public Task DeleteCarAdAsync(CarAd carAd);
        public Task<bool> CarAdWithIdExistsAsync(int id);
        public void DetachCarAd(CarAd? carAd);
        public Task<IList<int>> GetAllCarAdsIdsCreatedByUserWithIdAsync(int userId);
    }
}
