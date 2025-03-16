using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(FoodOrderDbContext context) : base(context) { }

        public async Task<Order> AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            return order;
        }

        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
        }

        public async Task<Customer?> GetCustomerByPhoneAsync(string phoneNumber)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }
    }
}