using CarSelling.Models;

namespace CarSelling.Repositories
{
    public interface ICarAdRepository
    {
        public Task<IEnumerable<CarAd>> GetAllCarAdsAsync();
        public Task<CarAd?> GetCarAdByIdAsync(int id);
        public Task CreateCarAdAsync(CarAd carAd);
        public Task UpdateCarAdAsync(CarAd carAd);
        public Task DeleteCarAdAsync(CarAd carAd);
        public Task<bool> CarAdWithIdExistsAsync(int id);
        public void DetachCarAd(CarAd? carAd);
    }
}
