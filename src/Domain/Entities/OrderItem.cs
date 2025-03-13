namespace Domain.Entities
{
    public class OrderItem
    {
        public long Id { get; private set; }
        public long OrderId { get; private set; }
        public long ItemId { get; private set; }
        public int Quantity { get; private set; }

        public OrderItem(long orderId, long itemId, int quantity)
        {
            OrderId = orderId;
            ItemId = itemId;
            Quantity = quantity;
        }
    }
}