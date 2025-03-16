using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task SaveChangesAsync();
        Task<Order> AddOrderAsync(Order order);
        Task AddOrderItemAsync(OrderItem orderItem);
        Task<Customer?> GetCustomerByPhoneAsync(string phoneNumber);
        Task AddCustomerAsync(Customer customer);
        Task<Order?> GetByIdAsync(long id);
        void Update(Order order);
    }
}