using Lesson11.Models;
using Lesson11.Response;

namespace Lesson11.Stores.Categories
{
    public interface ICategoryDataStore
    {
        public GetCategoryResponse? GetCategories(string? searchString);
		public Category? GetCategory(int id);
        public Category? CreateCategory(Category name);
        public Category? UpdateCategory(Category category);
        public void DeleteCategory(int id);
    }
}
