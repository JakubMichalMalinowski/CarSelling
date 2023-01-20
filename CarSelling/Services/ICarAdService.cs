using CarSelling.Models;
using System.Security.Claims;

namespace CarSelling.Services
{
    public interface ICarAdService
    {
        public Task<IEnumerable<CarAdDto>> GetAllCarAdsAsync();
        public Task<CarAdDto?> GetCarAdByIdAsync(int id);
        public Task CreateCarAdAsync(CarAdDto carAdDto);
        public Task UpdateCarAdAsync(int id, CarAdDto carAdDto);
        public Task DeleteCarAdAsync(int id);
    }
}
