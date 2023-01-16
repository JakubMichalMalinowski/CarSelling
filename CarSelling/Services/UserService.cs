using CarSelling.Infrastructure;
using CarSelling.Models;
using CarSelling.Repositories;
using Microsoft.AspNetCore.Identity;

namespace CarSelling.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> RegisterUserAsync(UserCreationDto userCreationDto)
        {
            if (await _repository.UserWithUserNameExists(userCreationDto.UserName))
            {
                return false;
            }

            var passwordHasher = new PasswordHasher<User>();

            var user = new User
            {
                UserName = userCreationDto.UserName
            };

            user.HashedPassword = passwordHasher
                .HashPassword(user, userCreationDto.Password);

            await _repository.CreateUser(user);
            return true;
        }
    }
}
