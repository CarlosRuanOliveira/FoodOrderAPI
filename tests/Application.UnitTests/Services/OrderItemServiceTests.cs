using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Services;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Moq;
using Xunit;

namespace Application.UnitTests.Services;

public class OrderItemServiceTests
{
    private readonly Mock<IOrderItemRepository> _mockOrderItemRepository;
    private readonly Mock<IOrderRepository> _mockOrderRepository;
    private readonly Mock<IMenuItemRepository> _mockMenuItemRepository;
    private readonly OrderItemService _orderItemService;

    public OrderItemServiceTests()
    {
        _mockOrderItemRepository = new Mock<IOrderItemRepository>();
        _mockOrderRepository = new Mock<IOrderRepository>();
        _mockMenuItemRepository = new Mock<IMenuItemRepository>();
        _orderItemService = new OrderItemService(
            _mockOrderItemRepository.Object,
            _mockOrderRepository.Object,
            _mockMenuItemRepository.Object
        );
    }

    [Fact]
    public async Task UpdateOrderItemsAsync_AddNewItem_UpdatesTotalPrice()
    {
        var orderId = 1;
        var request = new UpdateOrderItemsDTO
        {
            OrderItems = new List<OrderItemDTO> { new() { ItemId = 1, Quantity = 3 } }
        };

        var order = new Order(new Customer("John", "Doe", "123456789"));
        var menuItem = new MenuItem("Pizza", 2500);

        typeof(MenuItem).GetProperty("Id")?.SetValue(menuItem, 1);

        _mockOrderRepository.Setup(x => x.GetByIdAsync(orderId))
            .ReturnsAsync(order);
        _mockMenuItemRepository.Setup(x => x.GetMenuItemByIdsAsync(It.IsAny<IEnumerable<long>>()))
            .ReturnsAsync(new List<MenuItem> { menuItem });
        _mockOrderItemRepository.Setup(x => x.GetByOrderIdAsync(orderId))
            .ReturnsAsync(new List<OrderItem>());

        var result = await _orderItemService.UpdateOrderItemsAsync(orderId, request);

        Assert.Equal(7500, result.TotalPriceCents);
        _mockOrderItemRepository.Verify(x => x.AddOrderItemAsync(It.IsAny<OrderItem>()), Times.Once);
    }
}