using CarSelling.Models;

namespace CarSelling.Repositories
{
    public interface ICarRepository
    {
        public void DetachCar(Car car);
        public Task DeleteCarAsync(Car car);
    }
}
