using Application.DTOs.Request;
using Application.Services;
using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace Application.UnitTests.Services;

public class UserServiceTests
{
    private readonly Mock<UserManager<AppUser>> _mockUserManager;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        var store = new Mock<IUserStore<AppUser>>();
        _mockUserManager = new Mock<UserManager<AppUser>>(
            store.Object, null, null, null, null, null, null, null, null);

        _userService = new UserService(_mockUserManager.Object);
    }

    [Fact]
    public async Task CreateUserAsync_ValidRequest_ReturnsUserId()
    {
        var request = new CreateUserDTO
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            PhoneNumber = "123456789",
            Password = "SenhaForte123!"
        };

        _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), request.Password))
            .ReturnsAsync(IdentityResult.Success)
            .Callback<AppUser, string>((user, _) => user.Id = 123);

        var result = await _userService.CreateUserAsync(request);

        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        _mockUserManager.Verify(x => x.CreateAsync(It.IsAny<AppUser>(), request.Password), Times.Once);
    }

    [Fact]
    public async Task CreateUserAsync_InvalidPassword_ThrowsException()
    {
        var request = new CreateUserDTO
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            PhoneNumber = "123456789",
            Password = "senhafraca"
        };

        var errors = new[] { new IdentityError { Description = "Password too weak" } };

        _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), request.Password))
            .ReturnsAsync(IdentityResult.Failed(errors));

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _userService.CreateUserAsync(request));
        Assert.Contains("Password too weak", exception.Message);
    }

    [Fact]
    public async Task AuthenticateAsync_ValidCredentials_ReturnsUser()
    {
        var email = "john@example.com";
        var password = "SenhaForte123!";
        var user = new AppUser("John", "Doe", "123456789", email);

        _mockUserManager.Setup(x => x.FindByEmailAsync(email))
            .ReturnsAsync(user);

        _mockUserManager.Setup(x => x.CheckPasswordAsync(user, password))
            .ReturnsAsync(true);

        var result = await _userService.AuthenticateAsync(email, password);

        Assert.NotNull(result);
        Assert.Equal(email, result.Email);
    }

    [Fact]
    public async Task AuthenticateAsync_InvalidCredentials_ReturnsNull()
    {
        var email = "john@example.com";
        var password = "SenhaIncorreta";

        _mockUserManager.Setup(x => x.FindByEmailAsync(email))
            .ReturnsAsync((AppUser)null);

        var result = await _userService.AuthenticateAsync(email, password);

        Assert.Null(result);
    }

    [Fact]
    public async Task AuthenticateAsync_IncorrectPassword_ReturnsNull()
    {
        var email = "john@example.com";
        var password = "SenhaIncorreta";
        var user = new AppUser("John", "Doe", "123456789", email);

        _mockUserManager.Setup(x => x.FindByEmailAsync(email))
            .ReturnsAsync(user);

        _mockUserManager.Setup(x => x.CheckPasswordAsync(user, password))
            .ReturnsAsync(false);

        var result = await _userService.AuthenticateAsync(email, password);

        Assert.Null(result);
    }
}