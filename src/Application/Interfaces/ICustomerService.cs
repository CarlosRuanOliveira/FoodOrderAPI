using Application.DTOs;

namespace Application.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerResponseDTO>> GetCustomersAsync(int page, int pageSize);
    }
}