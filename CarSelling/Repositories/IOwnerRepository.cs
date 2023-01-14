namespace CarSelling.Repositories
{
    public interface IOwnerRepository
    {
        public Task<bool> OwnerExistsAsync(int id);
    }
}
