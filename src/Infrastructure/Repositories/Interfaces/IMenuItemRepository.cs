using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IMenuItemRepository
    {
        Task SaveChangesAsync();
        Task<MenuItem?> GetMenuItemByIdAsync(long itemId);
        Task<MenuItem> AddMenuItemAsync(MenuItem menuItem);
        Task<MenuItem?> GetMenuItemByNameAsync(string name);
        void UpdateMenuItem(MenuItem menuItem);
        void DeleteMenuItem(MenuItem menuItem);
        Task<List<MenuItem>> GetMenuItemByIdsAsync(IEnumerable<long> itemIds);
        Task<List<MenuItem>> GetMenuItemsPagedAsync(int page, int pageSize);
    }
}