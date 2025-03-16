using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomersPagedAsync(int page, int pageSize);
    }
}