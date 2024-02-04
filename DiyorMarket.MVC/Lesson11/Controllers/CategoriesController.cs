using Lesson11.Stores.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Lesson11.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryDataStore _categoryDataStore;

        public CategoriesController(ICategoryDataStore categoryDataStore)
        {
            _categoryDataStore = categoryDataStore ?? throw new ArgumentNullException(nameof(categoryDataStore));
        }

          public IActionResult Index()
          {
            var categories = _categoryDataStore.GetCategories();

            ViewBag.Categories = categories.Data;
            ViewBag.PageSize = categories.PageSize;
            ViewBag.PageCount = categories.TotalPages;
            ViewBag.CurrentPage = categories.PageNumber;

            return View();
          }
    }
}
