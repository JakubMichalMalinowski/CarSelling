using CarSelling.Infrastructure;
using CarSelling.Models;
using CarSelling.Repositories;

namespace CarSelling.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> RegisterUser(UserDto userDto)
        {
            if (await _repository.UserWithUserNameExists(userDto.UserName))
            {
                return false;
            }

            await _repository.CreateUser(userDto.ToUser());
            return true;
        }
    }
}
