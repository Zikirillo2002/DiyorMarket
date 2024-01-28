using Lesson11.Stores.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Lesson11.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryDataStore _categoryDataStore;

        public CategoriesController(ICategoryDataStore categoryDataStore)
        {
            _categoryDataStore = categoryDataStore;
        }

        public IActionResult Index()
        {
            var result = _categoryDataStore.GetCategories();

            if (result is null)
            {
                return NotFound();
            }

            ViewBag.CategoriesCount = result.Data.Count();
            ViewBag.CurrentPage = result.PageNumber;
            ViewBag.PageSize = result.PageSize;
            ViewBag.HasNext = result.HasNextPage;
            ViewBag.HasPrevious = result.HasPreviousPage;
            ViewBag.TotalPages = result.TotalPages;

            return View(result.Data);
        }
    }
}
