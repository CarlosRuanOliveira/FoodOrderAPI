

using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class MenuItemResponseDTO
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public decimal PriceCents { get; set; }
    }
}