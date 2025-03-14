using Domain.Enums;

namespace Domain.Entities
{
    public class Order
    {
        public long Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalPriceCents { get; private set; }
        public required Customer Customer { get; set; }
        public DateTime? UpdatedAt { get; private set; }
        public long? UpdatedBy { get; private set; }
        public long CustomerId { get; private set; }
        public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

        public Order()
        {
        }

        public Order(Customer customer)
        {
            CreatedAt = DateTime.UtcNow;
            Status = OrderStatus.Pending;
            Customer = customer;
            CustomerId = customer.Id;
        }

        public void UpdateTotalPrice(decimal newTotal)
        {
            TotalPriceCents = newTotal;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddItem(OrderItem item)
        {
            OrderItems.Add(item);
        }
    }
}