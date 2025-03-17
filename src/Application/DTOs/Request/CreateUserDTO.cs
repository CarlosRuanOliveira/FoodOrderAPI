using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Request
{
    public class CreateUserDTO
    {
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required string PhoneNumber { get; set; }

        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}