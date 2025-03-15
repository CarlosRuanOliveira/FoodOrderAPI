using Application.DTOs;

namespace Application.Interfaces
{
    public interface IMenuItemService
    {
        Task<MenuItemResponseDTO> CreateMenuItemAsync(CreateMenuItemDTO request);
    }
}