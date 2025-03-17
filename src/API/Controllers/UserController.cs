using Application.DTOs.Request;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public UserController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO request)
        {
            var result = await _userService.CreateUserAsync(request);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var user = await _userService.AuthenticateAsync(request.Email, request.Password);

            if (user == null)
                return Unauthorized();

            var token = _jwtService.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }
}