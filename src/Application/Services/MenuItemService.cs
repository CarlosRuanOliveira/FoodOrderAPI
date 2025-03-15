using Application.DTOs;
using Application.Interfaces;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;
        public MenuItemService(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        public Task<MenuItemResponseDTO> CreateMenuItemAsync(CreateMenuItemDTO request)
        {
            throw new NotImplementedException();
        }
    }
}