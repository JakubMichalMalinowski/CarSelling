using CarSelling.Data;
using CarSelling.Models;

namespace CarSelling.Services
{
    public class CarAdService : ICarAdService
    {
        private readonly CarSellingDbContext _context;

        public CarAdService(CarSellingDbContext context)
        {
            _context = context;
        }

        public async Task CreateCarAdAsync(CarAd carAd)
        {
            await _context.CarAds.AddAsync(carAd);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<CarAd> GetAll() =>
            _context.CarAds;

        public async Task<CarAd?> GetByIdAsync(int id)
        {
            var ad = await _context.CarAds.FindAsync(id);
            return ad;
        }
    }
}
