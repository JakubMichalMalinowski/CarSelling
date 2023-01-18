using CarSelling.Models;

namespace CarSelling.Repositories
{
    public interface IUserRepository
    {
        public Task CreateUserAsync(User user);
        public Task<bool> UserWithUserNameExistsAsync(string userName);
        public Task<User?> GetUserWithUserNameAsync(string userName);
        public Task<User?> GetUserByIdAsync(int id);
    }
}
