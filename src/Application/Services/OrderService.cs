using Application.DTOs;
using Application.Interfaces;
using Application.Validators;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderResponseDTO> CreateOrderAsync(CreateOrderDTO request)
        {
            OrderValidator.ValidateCreateOrderDTO(request);

            var customer = await _orderRepository.GetCustomerByPhoneAsync(request.CustomerPhoneNumber);

            if (customer == null)
            {
                if (string.IsNullOrEmpty(request.CustomerFirstName))
                    throw new ArgumentException("O primeiro nome do cliente é obrigatório.");

                if (string.IsNullOrEmpty(request.CustomerLastName))
                    throw new ArgumentException("O sobrenome do cliente é obrigatório.");

                customer = new Customer(request.CustomerFirstName, request.CustomerLastName ?? string.Empty, request.CustomerPhoneNumber);
                await _orderRepository.AddCustomerAsync(customer);
            }

            var order = new Order(customer);

            await _orderRepository.AddOrderAsync(order);

            var orderItems = new List<OrderItemResponseDTO>();
            decimal totalPrice = 0;

            foreach (var item in request.OrderItems)
            {
                var menuItem = await _orderRepository.GetMenuItemByIdAsync(item.ItemId) ?? throw new ArgumentException($"Item com ID {item.ItemId} não encontrado no cardápio.");
                var orderItem = new OrderItem(order.Id, menuItem.Id, item.Quantity);
                await _orderRepository.AddOrderItemAsync(orderItem);

                totalPrice += menuItem.PriceCents * item.Quantity;

                orderItems.Add(new OrderItemResponseDTO
                {
                    ItemId = menuItem.Id,
                    Quantity = item.Quantity,
                    PriceCents = menuItem.PriceCents
                });
            }

            order.UpdateTotalPrice(totalPrice);
            await _orderRepository.SaveChangesAsync();

            return new OrderResponseDTO
            {
                OrderId = order.Id,
                CustomerId = customer.Id,
                TotalPriceCents = order.TotalPriceCents,
                OrderItems = orderItems
            };
        }
    }
}