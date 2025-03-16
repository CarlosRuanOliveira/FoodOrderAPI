
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class BaseRepository
    {
        protected readonly FoodOrderDbContext _context;

        protected BaseRepository(FoodOrderDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}