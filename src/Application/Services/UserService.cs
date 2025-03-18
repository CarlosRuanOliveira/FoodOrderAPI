using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Interfaces;
using Application.Validators;
using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserResponseDTO> CreateUserAsync(CreateUserDTO request)
        {
            UserValidator.ValidateCreateUserDTO(request);

            var user = new AppUser(request.FirstName, request.LastName, request.PhoneNumber, request.Email)
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new ArgumentException(string.Join("; ", result.Errors.Select(e => e.Description)));
            }

            return new UserResponseDTO
            {
                Id = user.Id
            };
        }

        public async Task<AppUser?> AuthenticateAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
                return user;

            return null;
        }
    }
}