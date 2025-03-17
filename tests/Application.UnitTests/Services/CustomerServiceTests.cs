using Application.DTOs.Response;
using Application.Services;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Moq;
using Xunit;

namespace Application.UnitTests.Services;

public class CustomerServiceTests
{
    private readonly Mock<ICustomerRepository> _mockCustomerRepository;
    private readonly CustomerService _customerService;

    public CustomerServiceTests()
    {
        _mockCustomerRepository = new Mock<ICustomerRepository>();
        _customerService = new CustomerService(_mockCustomerRepository.Object);
    }

    [Fact]
    public async Task GetCustomersAsync_ValidPage_ReturnsCustomers()
    {
        var customers = new List<Customer>
        {
            new("John", "Doe", "123456789"),
            new("Jane", "Smith", "987654321")
        };

        _mockCustomerRepository.Setup(x => x.GetCustomersPagedAsync(1, 10))
            .ReturnsAsync(customers);

        var result = await _customerService.GetCustomersAsync(1, 10);

        Assert.Equal(2, result.Count);
        Assert.IsType<CustomerResponseDTO>(result[0]);
    }
}