using Lesson11.Models;
using Lesson11.Response;

namespace Lesson11.Stores.Products
{
    public interface IProductDataStore
    {
        public GetProductResponse? GetProducts(string? searchString, int? categoryId, int pageNumber);
        public Product? GetProduct(int id);
        public Stream GetExportFile();
        public Product? CreateProduct(Product category);
        public Product? UpdateProduct(Product category);
        public void DeleteProduct(int id);
    }
}
