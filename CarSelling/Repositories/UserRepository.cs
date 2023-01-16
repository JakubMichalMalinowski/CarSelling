using CarSelling.Data;
using CarSelling.Models;
using Microsoft.EntityFrameworkCore;

namespace CarSelling.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CarSellingDbContext _context;

        public UserRepository(CarSellingDbContext context)
        {
            _context = context;
        }

        public async Task CreateUser(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserWithUserNameExists(string userName)
        {
            return await _context.Users
                .AnyAsync(user => user.UserName == userName);
        }
    }
}
