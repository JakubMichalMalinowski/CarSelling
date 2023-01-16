using CarSelling.Models;

namespace CarSelling.Repositories
{
    public interface IUserRepository
    {
        public Task CreateUser(User user);
        public Task<bool> UserWithUserNameExists(string userName);
    }
}
