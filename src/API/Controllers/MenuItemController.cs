using Application.DTOs.Request;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
            var menuItemResponse = await _menuItemService.CreateMenuItemAsync(request);
            return Ok(menuItemResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuItem([FromRoute] long id, [FromBody] UpdateMenuItemDTO request)
        {
            await _menuItemService.UpdateMenuItemAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem([FromRoute] long id)
        {
            await _menuItemService.DeleteMenuItemAsync(id);
            return NoContent();
        }
    }
}