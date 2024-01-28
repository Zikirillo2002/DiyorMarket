using DiyorMarket.Domain.DTOs.Product;
using DiyorMarket.Domain.Pagniation;
using DiyorMarket.ResourceParameters;

namespace DiyorMarket.Domain.Interfaces.Services
{
    public interface IProductService
    {
        PaginatedList<ProductDto> GetProducts(ProductResourceParameters productResourceParameters);
        ProductDto? GetProductById(int id);
        ProductDto CreateProduct(ProductForCreateDto productToCreate);
        void UpdateProduct(ProductForUpdateDto productToUpdate);
        void DeleteProduct(int id);
    }
}
