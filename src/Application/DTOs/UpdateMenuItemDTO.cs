using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class UpdateMenuItemDTO
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public decimal PriceCents { get; set; }
    }
}