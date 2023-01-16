using CarSelling.Models;

namespace CarSelling.Services
{
    public interface IUserService
    {
        public Task<bool> RegisterUser(UserDto userDto);
    }
}
