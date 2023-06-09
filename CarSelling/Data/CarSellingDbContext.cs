using CarSelling.Models;
using Microsoft.EntityFrameworkCore;

namespace CarSelling.Data
{
    public class CarSellingDbContext : DbContext
    {
        public CarSellingDbContext(DbContextOptions<CarSellingDbContext> options) : base(options) { }

        public DbSet<CarAd> CarAds { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Car> Cars { get; set; } = default!;
        public DbSet<FilePath> FilePaths { get; set; } = default!;
        public DbSet<EncodedFile> EncodedFiles { get; set; } = default!;
    }
}
