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
        private readonly UserPrincipal _userPrincipal;
        private readonly IAuthorizationService _authorizationService;

        public CarAdService(ICarAdRepository carAdRepository, UserPrincipal userPrincipal, IAuthorizationService authorizationService)
        {
            _carAdRepository = carAdRepository;
            _userPrincipal = userPrincipal;
            _authorizationService = authorizationService;
        }

        public async Task<CarAdSimpleResponseDto> CreateCarAdAsync(CarAdRequestDto carAdRequestDto)
        {
            var carAd = carAdRequestDto.ToCarAdWithUser(await _userPrincipal.GetUserAsync());
            await _carAdRepository.CreateCarAdAsync(carAd);
            return carAd.ToCarAdSimpleResponseDto();
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

            await _carAdRepository.DeleteCarAdAsync(ad);
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

        public async Task UpdateCarAdAsync(int id, CarAdRequestDto carAdRequestDto)
        {
            {
                var carAdFromDB = await _carAdRepository.GetCarAdByIdAsync(id);
                var authResult = await _authorizationService.AuthorizeAsync(
                    _userPrincipal.UserClaimsPrincipal, carAdFromDB, new ResourceOwnerRequirement());
                if (!authResult.Succeeded)
                {
                    throw new ForbidException();
                }
                
                _carAdRepository.DetachCarAd(carAdFromDB);
            }

            var carAdFromRequest = carAdRequestDto.ToCarAdWithId(id);
            await _carAdRepository.UpdateCarAdAsync(carAdFromRequest);
        }
    }
}