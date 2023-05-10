using CarSelling.Data;
using CarSelling.Models;

namespace CarSelling.Repositories
{
    public class FilePathRepository : IPhotoRepository<FilePath>
    {
        private readonly CarSellingDbContext _context;

        public FilePathRepository(CarSellingDbContext context)
        {
            _context = context;
        }

        public async Task<FilePath?> GetPhotoByIdAsync(int id)
        {
            return await _context.FilePaths
                .FindAsync(id);
        }
    }
}
