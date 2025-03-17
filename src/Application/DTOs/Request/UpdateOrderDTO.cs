using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs.Request
{
    public class UpdateOrderDTO
    {
        [Required]
        public OrderStatus Status { get; set; }
    }
}