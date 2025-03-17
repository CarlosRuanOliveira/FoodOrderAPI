using System.ComponentModel.DataAnnotations;

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
}