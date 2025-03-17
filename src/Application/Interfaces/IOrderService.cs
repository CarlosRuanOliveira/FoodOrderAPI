using Application.DTOs.Request;
using Application.DTOs.Response;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDTO> CreateOrderAsync(CreateOrderDTO request);
        Task UpdateOrderAsync(long orderId, UpdateOrderDTO request);
    }
}