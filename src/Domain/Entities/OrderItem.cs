namespace Domain.Entities
{
    public class OrderItem
    {
        public long Id { get; private set; }
        public long OrderId { get; private set; }
        public long ItemId { get; private set; }
        public long Quantity { get; private set; }

        public OrderItem(long orderId, long itemId, long quantity)
        {
            OrderId = orderId;
            ItemId = itemId;
            Quantity = quantity;
        }
    }
}