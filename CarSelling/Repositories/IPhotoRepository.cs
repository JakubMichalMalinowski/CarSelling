using CarSelling.Models;

namespace CarSelling.Repositories
{
    public interface IPhotoRepository<T>
    {
        public Task<T?> GetPhotoByIdAsync(int id);
    }
}
