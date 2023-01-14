using CarSelling.Models;

namespace CarSelling.Repositories
{
    public interface IUserRepository
    {
        public Task CreateUser(User user);
    }
}
