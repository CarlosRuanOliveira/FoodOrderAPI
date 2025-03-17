using Application.DTOs;
using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Errors;
using Application.Interfaces;
using Application.Validators;
using Domain.Entities;
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

        public async Task<MenuItemResponseDTO> UpdateMenuItemAsync(long menuItemId, UpdateMenuItemDTO request)
        {
            MenuItemValidator.ValidateUpdateMenuItemDTO(menuItemId, request);

            var menuItem = await _menuItemRepository.GetMenuItemByIdAsync(menuItemId) ?? throw new KeyNotFoundException(string.Format(ErrorMsg.MenuItemNotFound, menuItemId));
            menuItem.Update(request.Name, request.PriceCents);

            await _menuItemRepository.SaveChangesAsync();

            return new MenuItemResponseDTO
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                PriceCents = menuItem.PriceCents
            };
        }

        public async Task DeleteMenuItemAsync(long menuItemId)
        {
            var menuItem = await _menuItemRepository.GetMenuItemByIdAsync(menuItemId) ?? throw new KeyNotFoundException(string.Format(ErrorMsg.MenuItemNotFound, menuItemId));
            _menuItemRepository.DeleteMenuItem(menuItem);
            await _menuItemRepository.SaveChangesAsync();
        }
    }
}