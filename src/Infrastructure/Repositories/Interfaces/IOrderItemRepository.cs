using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        Task SaveChangesAsync();
        Task AddOrderItemAsync(OrderItem orderItem);
        void RemoveOrderItem(OrderItem orderItem);
        void UpdateOrderItem(OrderItem orderItem);
        Task<List<OrderItem>> GetByOrderIdAsync(long orderId);
    }
}