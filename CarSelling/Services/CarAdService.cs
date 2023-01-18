using CarSelling.Exceptions;
using CarSelling.Infrastructure;
using CarSelling.Models;
using CarSelling.Repositories;

namespace CarSelling.Services
{
    public class CarAdService : ICarAdService
    {
        private readonly ICarAdRepository _carAdRepository;
        private readonly IOwnerRepository _ownerRepository;

        public CarAdService(ICarAdRepository carAdRepository,
            IOwnerRepository ownerRepository)
        {
            _carAdRepository = carAdRepository;
            _ownerRepository = ownerRepository;
        }

        public async Task CreateCarAdAsync(CarAdDto carAdDto)
        {
            var carAd = carAdDto.ToCarAd();
            await _carAdRepository.CreateCarAdAsync(carAd);
            carAdDto.Id = carAd.Id;
        }

        public async Task DeleteCarAdAsync(int id)
        {
            var ad = await _carAdRepository.GetCarAdByIdAsync(id);
            if (ad is null)
            {
                throw new NotFoundException();
            }

            await _carAdRepository.DeleteCarAdAsync(ad);
        }

        public async Task<IEnumerable<CarAdDto>> GetAllCarAdsAsync()
        {
            return (await _carAdRepository.GetAllCarAdsAsync())
                .Select(ad => ad.ToCarAdDto());
        }

        public async Task<CarAdDto?> GetCarAdByIdAsync(int id)
        {
            return (await _carAdRepository.GetCarAdByIdAsync(id))
                .ToCarAdDtoNullable();
        }

        public async Task UpdateCarAdAsync(int id, CarAdDto carAdDto)
        {
            if (id != carAdDto.Id)
            {
                throw new BadRequestException();
            }

            if (! await _ownerRepository.OwnerExistsAsync(carAdDto.Owner.Id))
            {
                throw new NotFoundException();
            }

            await _carAdRepository.UpdateCarAdAsync(carAdDto.ToCarAd());
        }
    }
}