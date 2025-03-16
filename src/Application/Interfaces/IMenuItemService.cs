using Application.DTOs;

namespace Application.Interfaces
{
    public interface IMenuItemService
    {
        Task<MenuItemResponseDTO> CreateMenuItemAsync(CreateMenuItemDTO request);
        Task<MenuItemResponseDTO> UpdateMenuItemAsync(long id, UpdateMenuItemDTO request);
        Task DeleteMenuItemAsync(long menuItemId);
    }
}