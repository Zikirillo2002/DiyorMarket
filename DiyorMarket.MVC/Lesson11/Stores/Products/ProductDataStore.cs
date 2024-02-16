using Lesson11.Models;
using Lesson11.Response;
using Lesson11.Services;
using Lesson11.Stores.User;
using Newtonsoft.Json;
using System.Text;

namespace Lesson11.Stores.Products
{
    public class ProductDataStore : IProductDataStore
    {
        private readonly ApiClient _api;
        private readonly IUserDataStore _userDataStore;

        

        public ProductDataStore()
        {
            _api = new ApiClient();
        }

        public GetProductResponse? GetProducts(string? searchString, int? categoryId, int pageNumber)
        {
            StringBuilder query = new("");

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query.Append($"searchString={searchString}&");
            }

            if (categoryId != null)
            {
                query.Append($"categoryId={categoryId}&");
            }

            if(pageNumber != 0)
            {
                query.Append($"pageNumber={pageNumber}");
            }

            var response = _api.Get("products?" + query.ToString());

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch products.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<GetProductResponse>(json);

            return result;
        }

        public Product? GetProduct(int id)
        {
            var response = _api.Get($"products/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch products with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<Product>(json);

            return result;
        }

        public Product? CreateProduct(Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            var response = _api.Post("products", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating product.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<Product>(jsonResponse);
        }

        public Product? UpdateProduct(Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            var response = _api.Put("products", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating products.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<Product>(jsonResponse);
        }

        public void DeleteProduct(int id)
        {
            var response = _api.Delete($"products/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not delete products with id: {id}.");
            }
        }
    }
}
