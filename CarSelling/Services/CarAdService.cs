using CarSelling.Exceptions;
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

        public async Task CreateAsync(CarAd carAd)
        {
            await _carAdRepository.CreateCarAdAsync(carAd);
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

        public IEnumerable<CarAd> GetAll()
        {
            return _carAdRepository.GetAllCarAds();
        }

        public async Task<CarAd?> GetByIdAsync(int id)
        {
            return await _carAdRepository.GetCarAdByIdAsync(id);
        }

        public async Task UpdateAsync(int id, CarAd carAd)
        {
            if (id != carAd.Id)
            {
                throw new BadRequestException();
            }

            if (! await _ownerRepository.OwnerExistsAsync(carAd.Owner.Id))
            {
                throw new NotFoundException();
            }

            await _carAdRepository.UpdateCarAdAsync(carAd);
        }
    }
}