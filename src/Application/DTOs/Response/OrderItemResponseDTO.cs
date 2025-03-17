namespace Application.DTOs.Response
{
    public class OrderItemResponseDTO
    {
        public long ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceCents { get; set; }
    }
}