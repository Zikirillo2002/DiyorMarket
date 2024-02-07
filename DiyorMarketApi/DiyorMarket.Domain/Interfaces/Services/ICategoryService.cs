using DiyorMarket.Domain.DTOs.Category;
using DiyorMarket.Domain.ResourceParameters;
using DiyorMarket.Domain.Responses;

namespace DiyorMarket.Domain.Interfaces.Services
{
    public interface ICategoryService
    {
        GetCategoryResponse GetCategories(CategoryResourceParameters categoryResourceParameters);
        CategoryDto? GetCategoryById(int id);
        CategoryDto CreateCategory(CategoryForCreateDto categoryToCreate);
        CategoryDto UpdateCategory(CategoryForUpdateDto categoryToUpdate);
        void DeleteCategory(int id);
    }
}
