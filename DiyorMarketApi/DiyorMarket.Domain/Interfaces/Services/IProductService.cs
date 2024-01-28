using DiyorMarket.Domain.DTOs.Product;
using DiyorMarket.Domain.Pagniation;
using DiyorMarket.Domain.Responses;
using DiyorMarket.ResourceParameters;

namespace DiyorMarket.Domain.Interfaces.Services
{
    public interface IProductService
    {
        GetProductResponse GetProducts(ProductResourceParameters productResourceParameters);
        ProductDto? GetProductById(int id);
        ProductDto CreateProduct(ProductForCreateDto productToCreate);
        void UpdateProduct(ProductForUpdateDto productToUpdate);
        void DeleteProduct(int id);
    }
}
