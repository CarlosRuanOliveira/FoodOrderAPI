namespace Application.DTOs
{
    public class OrderResponseDTO
    {
        public long OrderId { get; set; }
        public long CustomerId { get; set; }
        public decimal TotalPriceCents { get; set; }
        public required List<OrderItemResponseDTO> OrderItems { get; set; }
    }

    public class OrderItemResponseDTO
    {
        public long ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceCents { get; set; }
    }
}