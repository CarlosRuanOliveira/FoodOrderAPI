

using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class CreateMenuItemDTO
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required decimal PriceCents { get; set; }
    }
}