using CarSelling.Data;
using CarSelling.Exceptions;
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

        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public void DetachUser(User? user)
        {
            if (user is not null)
            {
                _context.Entry(user).State = EntityState.Detached;
            }
        }

        public async Task<User?> GetUserByIdAsync(int id) =>
            await _context.Users.FindAsync(id);

        public async Task<User?> GetUserWithUserNameAsync(string userName) =>
            await _context.Users
            .FirstOrDefaultAsync(user => user.UserName == userName);

        public async Task UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await UserWithIdExistsAsync(user.Id))
                {
                    throw new NotFoundException();
                }

                throw;
            }
        }

        public async Task<bool> UserWithIdExistsAsync(int userId) =>
            await _context.Users.AnyAsync(user => user.Id == userId);

        public async Task<bool> UserWithUserNameExistsAsync(string userName) =>
            await _context.Users.AnyAsync(user => user.UserName == userName);
    }
}
