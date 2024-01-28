using AutoMapper;
using DiyorMarket.Domain.DTOs.SupplyItem;
using DiyorMarket.Domain.Entities;

namespace DiyorMarket.Domain.Mappings
{
    public class SupplyItemMappings : Profile
    {
        public SupplyItemMappings() 
        {
            CreateMap<SupplyItemDto, SupplyItem>();
            CreateMap<SupplyItem, SupplyItemDto>();
            CreateMap<SupplyItemForCreateDto, SupplyItem>();
            CreateMap<SupplyItemForUpdateDto, SupplyItem>();
        }
    }
}
