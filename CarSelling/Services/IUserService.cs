using CarSelling.Models;

namespace CarSelling.Services
{
    public interface IUserService
    {
        public Task<bool> RegisterUserAsync(UserCreationDto userDto);
    }
}
