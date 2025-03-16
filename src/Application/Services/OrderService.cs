using Application.DTOs;
using Application.Errors;
using Application.Interfaces;
using Application.Validators;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMenuItemRepository _menuItemRepository;

        public OrderService(IOrderRepository orderRepository, IMenuItemRepository menuItemRepository)
        {
            _orderRepository = orderRepository;
            _menuItemRepository = menuItemRepository;
        }

        public async Task<OrderResponseDTO> CreateOrderAsync(CreateOrderDTO request)
        {
            OrderValidator.ValidateCreateOrderDTO(request);

            var customer = await _orderRepository.GetCustomerByPhoneAsync(request.CustomerPhoneNumber);

            if (customer == null)
            {
                if (string.IsNullOrEmpty(request.CustomerFirstName))
                    throw new ArgumentException(string.Format(ErrorMsg.CustomerParamRequired, "Primeiro nome"));

                if (string.IsNullOrEmpty(request.CustomerLastName))
                    throw new ArgumentException(string.Format(ErrorMsg.CustomerParamRequired, "Sobrenome"));

                customer = new Customer(request.CustomerFirstName, request.CustomerLastName, request.CustomerPhoneNumber);
                await _orderRepository.AddCustomerAsync(customer);
                await _orderRepository.SaveChangesAsync();
            }

            var order = new Order(customer);

            await _orderRepository.AddOrderAsync(order);
            await _orderRepository.SaveChangesAsync();

            var orderItems = new List<OrderItemResponseDTO>();
            decimal totalPrice = 0;

            foreach (var item in request.OrderItems)
            {
                var menuItem = await _menuItemRepository.GetMenuItemByIdAsync(item.ItemId) ?? throw new KeyNotFoundException(string.Format(ErrorMsg.MenuItemNotFound, item.ItemId));
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

        public async Task UpdateOrderAsync(long orderId, UpdateOrderDTO request)
        {
            OrderValidator.ValidateUpdateOrder(request);

            var order = await _orderRepository.GetByIdAsync(orderId) ?? throw new KeyNotFoundException(string.Format(ErrorMsg.OrderNotFound, orderId));

            if (order.Status == request.Status)
                return;

            order.UpdateStatus(request.Status, 0);

            _orderRepository.Update(order);
            await _orderRepository.SaveChangesAsync();
        }
    }
}