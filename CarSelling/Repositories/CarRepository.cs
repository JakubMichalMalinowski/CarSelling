using CarSelling.Data;
using CarSelling.Models;

namespace CarSelling.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarSellingDbContext _context;

        public CarRepository(CarSellingDbContext context)
        {
            _context = context;
        }

        public async Task DeleteCarAsync(Car car)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        public void DetachCar(Car car)
        {
            _context.Entry(car).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
        }
    }
}
