using Lesson11.Models;
using Lesson11.Response;

namespace Lesson11.Stores.Categories
{
    public interface ICategoryDataStore
    {
        public GetCategoryResponse? GetCategories();
        public Category? GetCategory(int id);
        public Category? CreateCategory(Category category);
        public Category? UpdateCategory(Category category);
        public void DeleteCategory(int id);
    }
}
