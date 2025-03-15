using System.ComponentModel.DataAnnotations;
using Application.Errors;

namespace Application.DTOs
{
    public class CreateOrderDTO
    {
        [Required]
        public required string CustomerPhoneNumber { get; set; }

        public string? CustomerFirstName { get; set; }

        public string? CustomerLastName { get; set; }

        [Required]
        public required List<OrderItemDTO> OrderItems { get; set; }
    }

    public class OrderItemDTO
    {
        [Required]
        public long ItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMsg.InvalidQuantity)]
        public int Quantity { get; set; }
    }
}