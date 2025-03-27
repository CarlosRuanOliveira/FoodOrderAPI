using Application.DTOs.Request;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> UpdateOrder([FromRoute] long id, [FromBody] UpdateOrderDTO request)
        {
            await _orderService.UpdateOrderAsync(id, request);
            return NoContent();
        }

        [HttpPut("{id}/OrderItems")]
        [Authorize]
        public async Task<IActionResult> UpdateOrderItems([FromRoute] long id, [FromBody] UpdateOrderItemsDTO request)
        {
            var orderResponse = await _orderItemService.UpdateOrderItemsAsync(id, request);
            return Ok(orderResponse);
        }

        [HttpGet("/Orders")]
        [Authorize]
        public async Task<IActionResult> GetOrders([FromQuery] int page = 1, int pageSize = 10)
        {
            var orders = await _orderService.GetOrdersAsync(page, pageSize);
            return Ok(orders);
        }
    }
}