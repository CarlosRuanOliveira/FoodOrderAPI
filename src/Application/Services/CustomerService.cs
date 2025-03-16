using Application.DTOs;
using Application.Interfaces;
using Application.Validators;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<CustomerResponseDTO>> GetCustomersAsync(int page, int pageSize)
        {
            CustomerValidator.ValidatePageAndPageSize(page, pageSize);

            var customers = await _customerRepository.GetCustomersPagedAsync(page, pageSize);

            return customers.Select(c => new CustomerResponseDTO
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber
            }).ToList();
        }
    }
}