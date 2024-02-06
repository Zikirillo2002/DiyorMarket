using AutoMapper;
using DiyorMarket.Domain.DTOs.Sale;
using DiyorMarket.Domain.DTOs.SaleItem;
using DiyorMarket.Domain.Entities;

namespace DiyorMarket.Domain.Mappings
{
    public class SaleMappings : Profile
    {
        public SaleMappings()
        {
            CreateMap<SaleDto, Sale>();
            CreateMap<Sale, SaleDto>()
                .ForMember(x => x.TotalDue, r => r.MapFrom(x => x.SaleItems.Sum(p => p.Quantity * p.UnitPrice)));
            CreateMap<SaleForCreateDto, Sale>();
            CreateMap<SaleForUpdateDto, Sale>();
        }
    }
}
