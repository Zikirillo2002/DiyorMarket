using Lesson11.Models;
using Lesson11.Response;
using Lesson11.Services;
using Newtonsoft.Json;

namespace Lesson11.Stores.Categories
{
    public class CategoryDataStore : ICategoryDataStore
    {
        private readonly ApiClient _api;

        public CategoryDataStore()
        {
            _api = new ApiClient();
        }

        public GetCategoryResponse? GetCategories()
        {
            var response = _api.Get("categories");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch categories.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<GetCategoryResponse>(json);

            return result;
        }

        public Category? GetCategory(int id)
        {
            var response = _api.Get($"categories/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch category with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<Category>(json);

            return result;
        }

        public Category? CreateCategory(Category category)
        {
            var json = JsonConvert.SerializeObject(category);
            var response = _api.Post("categories", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating category.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            
            return JsonConvert.DeserializeObject<Category>(jsonResponse);
        }

        public Category? UpdateCategory(Category category)
        {
            var json = JsonConvert.SerializeObject(category);
            var response = _api.Put("categories", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating category.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<Category>(jsonResponse);
        }

        public void DeleteCategory(int id)
        {
            var response = _api.Delete($"categories/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not delete category with id: {id}.");
            }
        }
    }
}
