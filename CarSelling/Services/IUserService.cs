using CarSelling.Models;

namespace CarSelling.Services
{
    public interface IUserService
    {
        public Task RegisterUser(User user);
        public Task LoginUser(User user);
    }
}
