namespace Application.DTOs
{
    public class OrderItemResponseDTO
    {
        public long ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceCents { get; set; }
    }
}