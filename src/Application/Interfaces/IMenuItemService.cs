using Application.DTOs;

namespace Application.Interfaces
{
    public interface IMenuItemService
    {
        Task<MenuItemResponseDTO> CreateMenuItemAsync(CreateMenuItemDTO request);
        Task<MenuItemResponseDTO> UpdateMenuItemAsync(UpdateMenuItemDTO request);
        Task DeleteMenuItemAsync(long menuItemId);
    }
}