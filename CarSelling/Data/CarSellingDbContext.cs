using CarSelling.Models;
using Microsoft.EntityFrameworkCore;

namespace CarSelling.Data
{
    public class CarSellingDbContext : DbContext
    {
        public CarSellingDbContext(DbContextOptions<CarSellingDbContext> options) : base(options) { }

        public DbSet<CarAd> CarAds { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<FilePath> FilePaths { get; set; }
        public DbSet<EncodedFile> EncodedFiles { get; set; }
    }
}
