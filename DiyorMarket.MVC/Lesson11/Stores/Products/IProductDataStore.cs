using Lesson11.Models;
using Lesson11.Response;

namespace Lesson11.Stores.Products
{
    public interface IProductDataStore
    {
        public GetProductResponse? GetProduct();
        public Product? GetProduct(int id);
        public Product? CreateProduct(Product category);
        public Product? UpdateProduct(Product category);
        public void DeleteProduct(int id);
    }
}
