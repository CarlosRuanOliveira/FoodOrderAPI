using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Errors;
using Application.Interfaces;
using Application.Validators;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMenuItemRepository _menuItemRepository;

        public OrderItemService(IOrderItemRepository orderItemRepository, IOrderRepository orderRepository, IMenuItemRepository menuItemRepository)
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _menuItemRepository = menuItemRepository;
        }

        public async Task<OrderResponseDTO> UpdateOrderItemsAsync(long orderId, UpdateOrderItemsDTO request)
        {
            OrderItemValidator.ValidateUpdateOrderItemsDTO(orderId, request);

            var order = await _orderRepository.GetByIdAsync(orderId)
                        ?? throw new KeyNotFoundException(ErrorMsg.OrderNotFound);
            var currentItems = await _orderItemRepository.GetByOrderIdAsync(orderId);

            var updatedItems = request.OrderItems
                .Select(itemDto => new OrderItem(orderId, itemDto.ItemId, itemDto.Quantity))
                .ToList();

            foreach (var updatedItem in updatedItems)
            {
                var existingItem = currentItems.FirstOrDefault(x => x.ItemId == updatedItem.ItemId);

                if (existingItem == null)
                {
                    currentItems.Add(updatedItem);
                    await _orderItemRepository.AddOrderItemAsync(updatedItem);
                    continue;
                }

                existingItem!.UpdateQuantity(updatedItem.Quantity);
                _orderItemRepository.UpdateOrderItem(existingItem);

            }

            var itemsToRemove = currentItems.Where(x => !updatedItems.Any(y => y.ItemId == x.ItemId)).ToList();
            foreach (var itemToRemove in itemsToRemove)
            {
                currentItems.Remove(itemToRemove);
                _orderItemRepository.RemoveOrderItem(itemToRemove);
            }

            var menuItemIds = currentItems.Select(x => x.ItemId).Distinct();
            var menuItems = await _menuItemRepository.GetMenuItemByIdsAsync(menuItemIds);
            var menuItemsDict = menuItems.ToDictionary(m => m.Id);

            decimal newTotalPrice = currentItems.Sum(item =>
                item.Quantity * (menuItemsDict.TryGetValue(item.ItemId, out var menuItem) ? menuItem.PriceCents : 0));
            order.UpdateTotalPrice(newTotalPrice);

            order.UpdateItems(currentItems, 0);

            await _orderRepository.SaveChangesAsync();

            var orderItemDtos = currentItems.Select(item => new OrderItemResponseDTO
            {
                ItemId = item.ItemId,
                Quantity = item.Quantity,
                PriceCents = menuItemsDict.TryGetValue(item.ItemId, out var menuItem) ? menuItem.PriceCents * item.Quantity : 0
            }).ToList();

            return new OrderResponseDTO
            {
                OrderId = order.Id,
                CustomerId = order.CustomerId,
                TotalPriceCents = order.TotalPriceCents,
                OrderItems = orderItemDtos
            };
        }
    }
}