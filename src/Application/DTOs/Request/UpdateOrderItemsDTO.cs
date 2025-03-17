using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Request
{
    public class UpdateOrderItemsDTO
    {
        [Required]
        public required List<OrderItemDTO> OrderItems { get; set; }
    }
}