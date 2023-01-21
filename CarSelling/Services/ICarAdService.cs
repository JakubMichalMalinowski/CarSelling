using CarSelling.Models;
using System.Security.Claims;

namespace CarSelling.Services
{
    public interface ICarAdService
    {
        public Task<IEnumerable<CarAdSimpleResponseDto>> GetAllCarAdsAsync();
        public Task<CarAdResponseDto?> GetCarAdByIdAsync(int id);
        public Task<CarAdResponseDto> CreateCarAdAsync(CarAdRequestDto carAdDto);
        public Task UpdateCarAdAsync(int adId, CarAdRequestDto carAdDto);
        public Task DeleteCarAdAsync(int id);
    }
}
