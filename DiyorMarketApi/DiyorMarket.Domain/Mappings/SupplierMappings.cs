using AutoMapper;
using DiyorMarket.Domain.DTOs.Supplier;
using DiyorMarket.Domain.Entities;

namespace DiyorMarket.Domain.Mappings
{
    public class SupplierMappings : Profile
    {
        public SupplierMappings() 
        {
            CreateMap<SupplierDto, Supplier>()
                .PreserveReferences();
            CreateMap<Supplier, SupplierDto>()
                .PreserveReferences();
            CreateMap<SupplierForCreateDto, Supplier>();
            CreateMap<SupplierForUpdateDto, Supplier>();
        }
    }
}
