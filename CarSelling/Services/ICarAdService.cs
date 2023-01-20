using CarSelling.Models;
using System.Security.Claims;

namespace CarSelling.Services
{
    public interface ICarAdService
    {
        public Task<IEnumerable<CarAdResponseDto>> GetAllCarAdsAsync();
        public Task<CarAdResponseDto?> GetCarAdByIdAsync(int id);
        public Task<CarAdResponseDto> CreateCarAdAsync(CarAdRequestDto carAdDto);
        public Task UpdateCarAdAsync(int id, CarAdRequestDto carAdDto);
        public Task DeleteCarAdAsync(int id);
    }
}
