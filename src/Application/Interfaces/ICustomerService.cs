using Application.DTOs.Response;

namespace Application.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerResponseDTO>> GetCustomersAsync(int page, int pageSize);
    }
}