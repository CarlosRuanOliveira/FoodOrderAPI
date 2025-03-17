using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories.Interfaces;
using Moq;
using Xunit;

namespace Application.UnitTests.Services;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _mockOrderRepository;
    private readonly Mock<IMenuItemRepository> _mockMenuItemRepository;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _mockOrderRepository = new Mock<IOrderRepository>();
        _mockMenuItemRepository = new Mock<IMenuItemRepository>();
        _orderService = new OrderService(_mockOrderRepository.Object, _mockMenuItemRepository.Object);
    }

    [Fact]
    public async Task CreateOrderAsync_NewCustomer_CreatesCustomerAndOrder()
    {
        var request = new CreateOrderDTO
        {
            CustomerPhoneNumber = "123456789",
            CustomerFirstName = "John",
            CustomerLastName = "Doe",
            OrderItems = new List<OrderItemDTO> { new() { ItemId = 1, Quantity = 2 } }
        };

        _mockOrderRepository.Setup(x => x.GetCustomerByPhoneAsync(request.CustomerPhoneNumber))
            .ReturnsAsync((Customer)null);
        _mockMenuItemRepository.Setup(x => x.GetMenuItemByIdAsync(1))
            .ReturnsAsync(new MenuItem("Pizza", 2500));

        var result = await _orderService.CreateOrderAsync(request);

        Assert.NotNull(result);
        _mockOrderRepository.Verify(x => x.AddCustomerAsync(It.IsAny<Customer>()), Times.Once);
    }

    [Fact]
    public async Task UpdateOrderAsync_ValidId_UpdatesOrderStatus()
    {
        var orderId = 1;
        var request = new UpdateOrderDTO { Status = OrderStatus.Ready };
        var order = new Order(new Customer("John", "Doe", "123456789"));

        _mockOrderRepository.Setup(x => x.GetByIdAsync(orderId))
            .ReturnsAsync(order);

        await _orderService.UpdateOrderAsync(orderId, request);

        Assert.Equal(OrderStatus.Ready, order.Status);
        _mockOrderRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
}