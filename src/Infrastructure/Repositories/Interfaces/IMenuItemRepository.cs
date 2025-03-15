using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IMenuItemRepository
    {
        Task<MenuItem?> GetMenuItemByIdAsync(long itemId);
        Task<MenuItem> AddMenuItemAsync(MenuItem menuItem);
    }
}