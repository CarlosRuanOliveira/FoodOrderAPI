using System.ComponentModel.DataAnnotations;
using Application.Errors;

namespace Application.DTOs
{
    public class OrderItemDTO
    {
        [Required]
        public long ItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMsg.InvalidQuantity)]
        public int Quantity { get; set; }
    }
}