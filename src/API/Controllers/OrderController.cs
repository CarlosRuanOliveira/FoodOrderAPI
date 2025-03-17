using Application.DTOs.Request;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;

        public OrderController(IOrderService orderService, IOrderItemService orderItemService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO request)
        {
            var orderResponse = await _orderService.CreateOrderAsync(request);
            return Ok(orderResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] long id, [FromBody] UpdateOrderDTO request)
        {
            await _orderService.UpdateOrderAsync(id, request);
            return NoContent();
        }

        [HttpPut("{id}/OrderItems")]
        public async Task<IActionResult> UpdateOrderItems([FromRoute] long id, [FromBody] UpdateOrderItemsDTO request)
        {
            var orderResponse = await _orderItemService.UpdateOrderItemsAsync(id, request);
            return Ok(orderResponse);
        }
    }
}