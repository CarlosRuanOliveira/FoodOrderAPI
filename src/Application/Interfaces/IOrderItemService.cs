using Application.DTOs.Request;
using Application.DTOs.Response;

namespace Application.Interfaces
{
    public interface IOrderItemService
    {
        Task<OrderResponseDTO> UpdateOrderItemsAsync(long orderId, UpdateOrderItemsDTO request);
    }
}