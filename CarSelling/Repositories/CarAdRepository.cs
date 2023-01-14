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

        public IQueryable<CarAd> GetAllCarAds() =>
            _context.CarAds;

        public async Task<CarAd?> GetCarAdByIdAsync(int id)
        {
            var ad = await _context.CarAds.FindAsync(id);
            return ad;
        }

        public async Task UpdateCarAdAsync(CarAd carAd)
        {
            _context.Entry(carAd).State = EntityState.Modified;
            _context.Entry(carAd.Owner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await AdExistsAsync(carAd.Id))
                {
                    throw new NotFoundException();
                }

                throw;
            }
        }

        public async Task<bool> AdExistsAsync(int id) =>
            await _context.CarAds.AnyAsync(ad => ad.Id == id);
    }
}