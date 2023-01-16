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

        public async Task CreateAsync(CarAdDto carAdDto)
        {
            await _carAdRepository.CreateCarAdAsync(carAdDto.ToCarAd());
        }

        public async Task DeleteAsync(int id)
        {
            var ad = await _carAdRepository.GetCarAdByIdAsync(id);
            if (ad is null)
            {
                throw new NotFoundException();
            }

            await _carAdRepository.DeleteCarAdAsync(ad);
        }

        public async Task<IEnumerable<CarAdDto>> GetAllAsync()
        {
            return (await _carAdRepository.GetAllCarAds())
                .Select(ad => ad.ToCarAdDto());
        }

        public async Task<CarAdDto?> GetByIdAsync(int id)
        {
            return (await _carAdRepository.GetCarAdByIdAsync(id))
                .ToCarAdDtoNullable();
        }

        public async Task UpdateAsync(int id, CarAdDto carAdDto)
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