using Domain.Enums;

namespace Domain.Entities
{
    public class Order
    {
        public long Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalPriceCents { get; private set; }
        public Customer Customer { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public User? UpdatedBy { get; private set; }
        public long CustomerId { get; private set; }
        public ICollection<OrderItem> Items { get; private set; } = [];

        public Order(Customer customer)
        {
            CreatedAt = DateTime.UtcNow;
            Status = OrderStatus.Pending;
            Customer = customer;
        }
    }
}