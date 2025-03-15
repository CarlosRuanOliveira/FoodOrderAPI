namespace Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}