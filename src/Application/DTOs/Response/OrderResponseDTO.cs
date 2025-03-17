namespace Application.DTOs.Response
{
    public class OrderResponseDTO
    {
        public long OrderId { get; set; }
        public long CustomerId { get; set; }
        public decimal TotalPriceCents { get; set; }
        public required List<OrderItemResponseDTO> OrderItems { get; set; }
    }
}