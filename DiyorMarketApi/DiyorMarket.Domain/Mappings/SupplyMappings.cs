using AutoMapper;
using DiyorMarket.Domain.DTOs.Supply;
using DiyorMarket.Domain.Entities;

namespace DiyorMarket.Domain.Mappings
{
    public class SupplyMappings : Profile
    {
        public SupplyMappings() 
        {
            CreateMap<SupplyDto, Supply>();
            CreateMap<Supply, SupplyDto>();
            CreateMap<SupplyForCreateDto, Supply>();
            CreateMap<SupplyForUpdateDto, Supply>();
        }
    }
}
