﻿using AutoMapper;
using DiyorMarket.Domain.DTOs.Product;
using DiyorMarket.Domain.Entities;

namespace DiyorMarket.Domain.Mappings
{
    public class ProductMappings : Profile
    {
        public ProductMappings() 
        {
            CreateMap<Product, ProductDto>()
                .ForMember(x => x.SupplyPrice, r => r.MapFrom(x => x.Price))
                .ForMember(x => x.SalePrice, r => r.MapFrom(x => x.Price * (decimal)1.5));

            CreateMap<ProductDto, Product>();
            CreateMap<ProductForCreateDto, Product>()
                .ForMember(x => x.Price, r => r.MapFrom(x => x.SupplyPrice));
            CreateMap<ProductForUpdateDto, Product>()
                .ForMember(x => x.Price, r => r.MapFrom(x => x.SupplyPrice));
            CreateMap<Product, ProductForCreateDto>();
            CreateMap<Product, ProductForUpdateDto>();
        }
    }
}
