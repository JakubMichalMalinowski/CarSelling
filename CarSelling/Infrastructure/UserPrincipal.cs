using CarSelling.Exceptions;
using CarSelling.Models;
using CarSelling.Repositories;
using Microsoft.AspNetCore.Http;

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

        public async Task<User> GetUserAsync()
        {
            var userCP = _contextAccessor.HttpContext?.User;
            var idString = userCP?.FindFirst("userId")?.Value;
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
