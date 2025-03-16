using Application.DTOs;
using Application.Errors;
using Application.Interfaces;
using Application.Validators;
using Domain.Entities;
using Infrastructure.Persistence;
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

        public async Task<MenuItemResponseDTO> CreateMenuItemAsync(CreateMenuItemDTO request)
        {
            MenuItemValidator.ValidateCreateMenuItemDTO(request);

            var menuItem = await _menuItemRepository.GetMenuItemByNameAsync(request.Name);

            if (menuItem != null)
                throw new ArgumentException(string.Format(ErrorMsg.MenuItemAlreadyExists, request.Name, menuItem.Id));

            menuItem = new MenuItem(request.Name, request.PriceCents);

            await _menuItemRepository.AddMenuItemAsync(menuItem);
            await _menuItemRepository.SaveChangesAsync();

            return new MenuItemResponseDTO
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                PriceCents = menuItem.PriceCents
            };
        }
    }
}