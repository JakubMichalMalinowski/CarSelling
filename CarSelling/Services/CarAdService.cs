using CarSelling.Exceptions;
using CarSelling.Infrastructure;
using CarSelling.Models;
using CarSelling.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace CarSelling.Services
{
    public class CarAdService : ICarAdService
    {
        private readonly ICarAdRepository _carAdRepository;
        private readonly ICarRepository _carRepository;
        private readonly UserPrincipal _userPrincipal;
        private readonly IAuthorizationService _authorizationService;

        public CarAdService(ICarAdRepository carAdRepository,
            ICarRepository carRepository,
            UserPrincipal userPrincipal,
            IAuthorizationService authorizationService)
        {
            _carAdRepository = carAdRepository;
            _carRepository = carRepository;
            _userPrincipal = userPrincipal;
            _authorizationService = authorizationService;
        }

        public async Task<CarAdResponseDto> CreateCarAdAsync(CarAdRequestDto carAdRequestDto)
        {
            var carAd = carAdRequestDto.ToCarAdWithUser(await _userPrincipal.GetUserAsync());
            await _carAdRepository.CreateCarAdAsync(carAd);
            return carAd.ToCarAdResponseDto() ?? throw new NotFoundException();
        }

        public async Task DeleteCarAdAsync(int id)
        {
            var ad = await _carAdRepository.GetCarAdByIdAsync(id);
            if (ad is null)
            {
                throw new NotFoundException();
            }

            var authResult = await _authorizationService.AuthorizeAsync(
                _userPrincipal.UserClaimsPrincipal, ad, new ResourceOwnerRequirement());

            if (!authResult.Succeeded)
            {
                throw new ForbidException();
            }

            Car car = ad.Car;
            await _carAdRepository.DeleteCarAdAsync(ad);
            await _carRepository.DeleteCarAsync(car);
        }

        public async Task<IEnumerable<CarAdSimpleResponseDto>> GetAllCarAdsAsync()
        {
            return (await _carAdRepository.GetAllCarAdsAsync())
                .Select(ad => ad.ToCarAdSimpleResponseDto());
        }

        public async Task<CarAdResponseDto?> GetCarAdByIdAsync(int id)
        {
            return (await _carAdRepository.GetCarAdByIdAsync(id))
                .ToCarAdResponseDto();
        }

        public async Task UpdateCarAdAsync(int adId, CarAdRequestDto carAdRequestDto)
        {
            var carAdFromDB = await _carAdRepository.GetCarAdByIdAsync(adId);
            var authResult = await _authorizationService.AuthorizeAsync(
                _userPrincipal.UserClaimsPrincipal, carAdFromDB, new ResourceOwnerRequirement());
            if (!authResult.Succeeded)
            {
                throw new ForbidException();
            }

            if (carAdFromDB is null)
            {
                throw new NotFoundException();
            }
            var car = carAdFromDB.Car;
            int carId = car.Id;

            _carAdRepository.DetachCarAd(carAdFromDB);
            _carRepository.DetachCar(car);

            var carAdFromRequest = carAdRequestDto.ToCarAdWithIds(adId, carId);
            await _carAdRepository.UpdateCarAdAsync(carAdFromRequest);
        }
    }
}