using CarSelling.Data;
using CarSelling.Exceptions;
using CarSelling.Infrastructure;
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

        public async Task<IEnumerable<CarAdDto>> GetAllCarAdsAsync() =>
            await _context.CarAds
            .Select(c => new CarAdDto
            {
                Id = c.Id,
                Title = c.Title,
                Price = c.Price,
                Negotiable = c.Negotiable,
                PhotoPathIds = c.PhotoPaths!.Select(p => p.Id).ToArray(),
                EncodedPhotoIds = c.EncodedPhotos!.Select(p => p.Id).ToArray()
            })
            .ToListAsync();

        public async Task<CarAd?> GetCarAdAsync(int id)
        {
            return await _context.CarAds
                .Include(c => c.Car)
                .Include(c => c.PhotoPaths)
                .Include(c => c.EncodedPhotos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CarAdDto?> GetCarAdByIdAsync(int id)
        {
            var ad = _context.CarAds
                .Include(c => c.CreatedBy)
                .Include(c => c.Car)
                .Include(c => c.PhotoPaths)
                .Include(c => c.EncodedPhotos)
                .Where(c => c.Id == id)
                .Select(c => new CarAdDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    Price = c.Price,
                    Negotiable = c.Negotiable,
                    Car = c.Car,
                    CreatedBy = c.CreatedBy,
                    PhotoPathIds = c.PhotoPaths!.Select(p => p.Id).ToArray(),
                    EncodedPhotoIds = c.EncodedPhotos!.Select(p => p.Id).ToArray()
                });
            return await ad.FirstOrDefaultAsync();
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
                if (!await CarAdWithIdExistsAsync(carAd.Id))
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

        public async Task<IList<int>> GetAllCarAdsIdsCreatedByUserWithIdAsync(int userId)
        {
            return await _context.CarAds
                .Where(ad => ad.CreatedBy.Id == userId)
                .Select(ad => ad.Id)
                .ToListAsync();
        }
    }
}