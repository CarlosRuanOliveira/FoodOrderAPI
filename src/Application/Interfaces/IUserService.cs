using Application.DTOs.Request;
using Application.DTOs.Response;
using Infrastructure.Persistence.Identity;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDTO> CreateUserAsync(CreateUserDTO request);
        Task<AppUser?> AuthenticateAsync(string email, string password);
    }
}