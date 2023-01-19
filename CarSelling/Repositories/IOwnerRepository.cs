namespace CarSelling.Repositories
{
    public interface IOwnerRepository
    {
        public Task<bool> OwnerWithIdExistsAsync(int id);
    }
}
