﻿using Lesson11.Models;
using Lesson11.Response;
using Lesson11.Services;
using Newtonsoft.Json;

namespace Lesson11.Stores.Products
{
    public class ProductDataStore : IProductDataStore
    {
        private readonly ApiClient _api;

        public ProductDataStore()
        {
            _api = new ApiClient();
        }

        public GetProductResponse? GetProducts()
        {
            var response = _api.Get("products");

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
            var response = _api.Post("customers", json);

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