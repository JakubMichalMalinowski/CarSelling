using CarSelling.Exceptions;
using CarSelling.Models;
using CarSelling.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CarSelling.Infrastructure
{
    public class UserPrincipal
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserPrincipal(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _contextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal UserClaimsPrincipal
        {
            get => _contextAccessor.HttpContext?.User ?? throw new UnauthorizedException();
        }

        public async Task<User> GetUserAsync()
        {
            var idString = UserClaimsPrincipal.FindFirst("userId")?.Value;
            var success = int.TryParse(idString, out var userId);
            if (!success)
            {
                throw new UnauthorizedException();
            }

            return await _userRepository.GetUserByIdAsync(userId)
                ?? throw new UnauthorizedException();
        }
    }
}
