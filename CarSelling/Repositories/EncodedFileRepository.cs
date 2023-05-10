using CarSelling.Data;
using CarSelling.Models;

namespace CarSelling.Repositories
{
    public class EncodedFileRepository : IPhotoRepository<EncodedFile>
    {
        private readonly CarSellingDbContext _context;

        public EncodedFileRepository(CarSellingDbContext context)
        {
            _context = context;
        }

        public async Task<EncodedFile?> GetPhotoByIdAsync(int id)
        {
            return await _context.EncodedFiles.FindAsync(id);
        }
    }
}
