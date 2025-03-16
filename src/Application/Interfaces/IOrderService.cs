using Application.DTOs;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDTO> CreateOrderAsync(CreateOrderDTO request);
        Task UpdateOrderAsync(long orderId, UpdateOrderDTO request);
    }
}