using CarSelling.Data;
using Microsoft.EntityFrameworkCore;

namespace CarSelling.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly CarSellingDbContext _context;

        public OwnerRepository(CarSellingDbContext context)
        {
            _context = context;
        }

        public async Task<bool> OwnerWithIdExistsAsync(int id) =>
            await _context.Owners.AnyAsync(owner => owner.Id == id);
    }
}
