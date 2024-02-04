using AutoMapper;
using DiyorMarket.Domain.DTOs.Customer;
using DiyorMarket.Domain.Entities;

namespace DiyorMarket.Domain.Mappings
{
    internal class CustomerMappings : Profile
    {
        public CustomerMappings() 
        {
            CreateMap<Customer, CustomerDto>()
                .ForCtorParam(nameof(CustomerDto.FullName),
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<CustomerDto, Customer>();
            CreateMap<CustomerForCreateDto, Customer>();
            CreateMap<CustomerForUpdateDto, Customer>();
        }
    }
}
