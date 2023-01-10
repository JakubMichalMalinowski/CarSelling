using CarSelling.Data;
using CarSelling.Exceptions;
using CarSelling.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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

        public async Task DeleteCarAdAsync(int id)
        {
            var ad = await _context.CarAds.FindAsync(id);
            if (ad is null)
            {
                throw new NotFoundException();
            }

            _context.CarAds.Remove(ad);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<CarAd> GetAll() =>
            _context.CarAds;

        public async Task<CarAd?> GetByIdAsync(int id)
        {
            var ad = await _context.CarAds.FindAsync(id);
            return ad;
        }

        public async Task UpdateCarAdAsync(CarAd carAd)
        {
            if (!OwnerExists(carAd.Owner.Id))
            {
                throw new NotFoundException();
            }

            _context.Entry(carAd).State = EntityState.Modified;
            _context.Entry(carAd.Owner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdExists(carAd.Id))
                {
                    throw new NotFoundException();
                }

                throw;
            }
        }

        private bool AdExists(int id) =>
            _context.CarAds.Any(ad => ad.Id == id);

        private bool OwnerExists(int id) =>
            _context.Owners.Any(owner => owner.Id == id);
    }
}
