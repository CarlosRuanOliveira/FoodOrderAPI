using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

namespace Domain.Entities
{
    public class Order
    {
        public long Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalPriceCents { get; private set; }
        public Customer Customer { get; private set; } = default!;
        public DateTime? UpdatedAt { get; private set; }
        public long? UpdatedBy { get; private set; }
        public long CustomerId { get; private set; }
        public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

        private Order() { }

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

        public void UpdateStatus(OrderStatus newStatus, long updatedBy)
        {
            Status = newStatus;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = updatedBy;
        }

        public void UpdateItems(List<OrderItem> updatedItems, long updatedBy)
        {
            OrderItems = updatedItems;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = updatedBy;
        }
    }
}