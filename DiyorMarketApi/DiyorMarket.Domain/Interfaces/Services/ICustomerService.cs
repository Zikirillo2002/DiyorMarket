using DiyorMarket.Domain.DTOs.Customer;
using DiyorMarket.Domain.Pagniation;
using DiyorMarket.Domain.ResourceParameters;
using DiyorMarket.Domain.Responses;

namespace DiyorMarket.Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        GetCustomerResponse GetCustomers(CustomerResourceParameters customerResourceParameters);
        CustomerDto? GetCustomerById(int id);
        CustomerDto CreateCustomer(CustomerForCreateDto customerToCreate);
        void UpdateCustomer(CustomerForUpdateDto customerToUpdate);
        void DeleteCustomer(int id);
    }
}
