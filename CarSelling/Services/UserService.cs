using CarSelling.Data;
using CarSelling.Models;

namespace CarSelling.Services
{
    public class UserService : IUserService
    {
        private readonly CarSellingDbContext _context;

        public UserService(CarSellingDbContext context)
        {
            _context= context;
        }

        public Task LoginUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task RegisterUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
