using Application.DTOs;
using Application.Errors;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenuItem([FromBody] CreateMenuItemDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var menuItemResponse = await _menuItemService.CreateMenuItemAsync(request);
                return Ok(menuItemResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ErrorMsg.InvalidRequest, message = ex.Message });
            }
        }
    }
}