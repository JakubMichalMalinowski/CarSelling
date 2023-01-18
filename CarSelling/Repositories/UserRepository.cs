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

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id) =>
            await _context.Users.FindAsync(id);

        public async Task<User?> GetUserWithUserNameAsync(string userName) =>
            await _context.Users
            .FirstOrDefaultAsync(user => user.UserName == userName);

        public async Task<bool> UserWithUserNameExistsAsync(string userName) =>
            await _context.Users.AnyAsync(user => user.UserName == userName);
    }
}
