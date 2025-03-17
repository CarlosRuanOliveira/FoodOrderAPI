using Application.DTOs.Request;
using Application.Services;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Moq;
using Xunit;

namespace Application.UnitTests.Services;

public class MenuItemServiceTests
{
    private readonly Mock<IMenuItemRepository> _mockMenuItemRepository;
    private readonly MenuItemService _menuItemService;

    public MenuItemServiceTests()
    {
        _mockMenuItemRepository = new Mock<IMenuItemRepository>();
        _menuItemService = new MenuItemService(_mockMenuItemRepository.Object);
    }

    [Fact]
    public async Task CreateMenuItemAsync_ValidRequest_ReturnsMenuItem()
    {
        var request = new CreateMenuItemDTO { Name = "Pizza", PriceCents = 2500 };
        var menuItem = new MenuItem(request.Name, request.PriceCents);

        _mockMenuItemRepository.Setup(x => x.GetMenuItemByNameAsync(request.Name))
            .ReturnsAsync((MenuItem)null);

        _mockMenuItemRepository.Setup(x => x.AddMenuItemAsync(It.IsAny<MenuItem>()))
            .ReturnsAsync((MenuItem m) => m);

        var result = await _menuItemService.CreateMenuItemAsync(request);

        Assert.NotNull(result);
        Assert.Equal(request.Name, result.Name);
        _mockMenuItemRepository.Verify(x => x.AddMenuItemAsync(It.IsAny<MenuItem>()), Times.Once);
    }

    [Fact]
    public async Task UpdateMenuItemAsync_InvalidId_ThrowsKeyNotFoundException()
    {
        var menuItemId = 999;
        var request = new UpdateMenuItemDTO { Name = "Pizza", PriceCents = 3000 };

        _mockMenuItemRepository.Setup(x => x.GetMenuItemByIdAsync(menuItemId))
            .ReturnsAsync((MenuItem)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _menuItemService.UpdateMenuItemAsync(menuItemId, request));
    }

    [Fact]
    public async Task DeleteMenuItemAsync_ValidId_DeletesMenuItem()
    {
        var menuItemId = 1;
        var menuItem = new MenuItem("Pizza", 2500);

        _mockMenuItemRepository.Setup(x => x.GetMenuItemByIdAsync(menuItemId))
            .ReturnsAsync(menuItem);

        await _menuItemService.DeleteMenuItemAsync(menuItemId);

        _mockMenuItemRepository.Verify(x => x.DeleteMenuItem(menuItem), Times.Once);
    }
}