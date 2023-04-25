using CarSelling.Data;
using CarSelling.Exceptions;
using CarSelling.Models;
using Microsoft.EntityFrameworkCore;

namespace CarSelling.Repositories
{
    public class CarAdRepository : ICarAdRepository
    {
        private readonly CarSellingDbContext _context;

        public CarAdRepository(CarSellingDbContext context)
        {
            _context = context;
        }

        public async Task CreateCarAdAsync(CarAd carAd)
        {
            await _context.CarAds.AddAsync(carAd);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCarAdAsync(CarAd carAd)
        {
            _context.CarAds.Remove(carAd);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarAd>> GetAllCarAdsAsync() =>
            await _context.CarAds.ToListAsync();

        public async Task<CarAd?> GetCarAdByIdAsync(int id)
        {
            var ad = await _context.CarAds.Include(c => c.CreatedBy)
                .Include(c => c.Car)
                .Include(c => c.PhotoPaths)
                .FirstOrDefaultAsync(c => c.Id == id);
            return ad;
        }

        public async Task UpdateCarAdAsync(CarAd carAd)
        {
            _context.Update(carAd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await CarAdWithIdExistsAsync(carAd.Id))
                {
                    throw new NotFoundException();
                }

                throw;
            }
        }

        public async Task<bool> CarAdWithIdExistsAsync(int id) =>
            await _context.CarAds.AnyAsync(ad => ad.Id == id);

        public void DetachCarAd(CarAd? carAd)
        {
            if (carAd is not null)
            {
                _context.Entry(carAd).State = EntityState.Detached;
            }
        }
    }
}