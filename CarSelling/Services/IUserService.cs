using CarSelling.Models;

namespace CarSelling.Services
{
    public interface IUserService
    {
        public Task<UserResponseDto> RegisterUserAsync(UserRequestDto userDto);
        public Task<string> LoginUserAsync(UserRequestDto userDto);
        public Task<UserResponseDto?> GetUserByIdAsync(int id);
    }
}
