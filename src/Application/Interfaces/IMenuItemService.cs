using Application.DTOs.Request;
using Application.DTOs.Response;

namespace Application.Interfaces
{
    public interface IMenuItemService
    {
        Task<MenuItemResponseDTO> CreateMenuItemAsync(CreateMenuItemDTO request);
        Task<MenuItemResponseDTO> UpdateMenuItemAsync(long id, UpdateMenuItemDTO request);
        Task DeleteMenuItemAsync(long menuItemId);
    }
}