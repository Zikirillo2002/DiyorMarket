using AutoMapper;
using DiyorMarket.Domain.DTOs.Product;
using DiyorMarket.Domain.Entities;

namespace DiyorMarket.Domain.Mappings
{
    public class ProductMappings : Profile
    {
        public ProductMappings() 
        {
            CreateMap<Product, ProductDto>()
                //.ForCtorParam(nameof(ProductDto.SalePrice), s => s.MapFrom(nameof(Product.Price)))
                //.ForCtorParam(nameof(ProductDto.SupplyPrice), s => s.MapFrom(s => s.Price * (decimal)0.8))
                .ForMember(x => x.SupplyPrice, r => r.MapFrom(x => x.Price))
                .ForMember(x => x.SalePrice, r => r.MapFrom(x => x.Price * (decimal)1.2));

            CreateMap<ProductDto, Product>();
            CreateMap<ProductForCreateDto, Product>()
                .ForMember(x => x.Price, r => r.MapFrom(x => x.SupplyPrice));
            CreateMap<ProductForUpdateDto, Product>();
            CreateMap<Product, ProductForCreateDto>();
            CreateMap<Product, ProductForUpdateDto>();
        }
    }
}
