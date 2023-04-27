using CarSelling.Models;

namespace CarSelling.Repositories
{
    public interface IFilePathRepository
    {
        public Task<FilePath?> GetFilePathByIdAsync(int id);
    }
}
