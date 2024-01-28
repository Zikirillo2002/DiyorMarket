using AutoMapper;
using DiyorMarket.Domain.DTOs.SaleItem;
using DiyorMarket.Domain.Entities;

namespace DiyorMarket.Domain.Mappings
{
    public class SaleItemMappings : Profile
    {
        public SaleItemMappings() 
        {
            CreateMap<SaleItemDto, SaleItem>();
            CreateMap<SaleItem, SaleItemDto>();
            CreateMap<SaleItemForCreateDto, SaleItem>();
            CreateMap<SaleItemForUpdateDto, SaleItem>();
        }
    }
}
