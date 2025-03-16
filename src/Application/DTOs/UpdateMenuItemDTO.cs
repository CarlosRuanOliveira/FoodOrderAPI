using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class UpdateMenuItemDTO
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public decimal PriceCents { get; set; }
    }
}