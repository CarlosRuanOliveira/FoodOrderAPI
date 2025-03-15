using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly FoodOrderDbContext _context;

        public MenuItemRepository(FoodOrderDbContext context)
        {
            _context = context;
        }

        public async Task<MenuItem> AddMenuItemAsync(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();
            return menuItem;
        }

        public async Task<MenuItem?> GetMenuItemByIdAsync(long itemId)
        {
            return await _context.MenuItems.FirstOrDefaultAsync(m => m.Id == itemId);
        }
    }
}