using DiyorMarket.Domain.DTOs.Customer;
using DiyorMarket.Domain.Pagniation;
using DiyorMarket.Domain.ResourceParameters;

namespace DiyorMarket.Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        PaginatedList<CustomerDto> GetCustomers(CustomerResourceParameters customerResourceParameters);
        CustomerDto? GetCustomerById(int id);
        CustomerDto CreateCustomer(CustomerForCreateDto customerToCreate);
        void UpdateCustomer(CustomerForUpdateDto customerToUpdate);
        void DeleteCustomer(int id);
    }
}
