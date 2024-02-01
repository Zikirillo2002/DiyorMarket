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
                .ForCtorParam(nameof(ProductDto.SalePrice), s => s.MapFrom(nameof(Product.Price)))
                .ForCtorParam(nameof(ProductDto.SupplyPrice), s => s.MapFrom(s => s.Price*(decimal)0.8));
            CreateMap<ProductDto, Product>();
            CreateMap<ProductForCreateDto, Product>();
            CreateMap<ProductForUpdateDto, Product>();
        }
    }
}
