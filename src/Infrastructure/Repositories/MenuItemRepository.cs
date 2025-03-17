using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MenuItemRepository : BaseRepository, IMenuItemRepository
    {
        public MenuItemRepository(FoodOrderDbContext context) : base(context) { }

        public async Task<MenuItem> AddMenuItemAsync(MenuItem menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);
            return menuItem;
        }

        public async Task<MenuItem?> GetMenuItemByIdAsync(long itemId)
        {
            return await _context.MenuItems.FirstOrDefaultAsync(m => m.Id == itemId);
        }

        public async Task<MenuItem?> GetMenuItemByNameAsync(string name)
        {
            return await _context.MenuItems.FirstOrDefaultAsync(m => m.Name == name);
        }

        public void UpdateMenuItem(MenuItem menuItem)
        {
            _context.MenuItems.Update(menuItem);
        }

        public void DeleteMenuItem(MenuItem menuItem)
        {
            _context.MenuItems.Remove(menuItem);
        }

        public async Task<List<MenuItem>> GetMenuItemByIdsAsync(IEnumerable<long> itemIds)
        {
            return await _context.MenuItems
                                 .Where(m => itemIds.Contains(m.Id))
                                 .ToListAsync();
        }
    }
}