using CarSelling.Data;
using CarSelling.Models;

namespace CarSelling.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CarSellingDbContext _context;

        public UserRepository(CarSellingDbContext context)
        {
            _context = context;
        }

        public Task CreateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
