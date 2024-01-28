using AutoMapper;
using DiyorMarket.Domain.DTOs.Supplier;
using DiyorMarket.Domain.Entities;

namespace DiyorMarket.Domain.Mappings
{
    public class SupplierMappings : Profile
    {
        public SupplierMappings() 
        {
            CreateMap<SupplierDto, Supplier>();
            CreateMap<Supplier, SupplierDto>();
            CreateMap<SupplierForCreateDto, Supplier>();
            CreateMap<SupplierForUpdateDto, Supplier>();
        }
    }
}
