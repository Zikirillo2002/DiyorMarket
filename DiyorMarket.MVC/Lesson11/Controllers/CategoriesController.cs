using Bogus.DataSets;
using ExcelDataReader;
using Lesson11.Models;

namespace Lesson11.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryDataStore _categoryDataStore;

        public CategoriesController(ICategoryDataStore categoryDataStore)
        {
            _categoryDataStore = categoryDataStore ?? throw new ArgumentNullException(nameof(categoryDataStore));
        }

        public IActionResult Index(string searchString, int? pageNumber)
        {
            //int page = string.IsNullOrWhiteSpace(pageNumber) ? 1 : int.Parse(pageNumber);

            var categories = _categoryDataStore.GetCategories(searchString, pageNumber);

            ViewBag.Categories = categories.Data;
            ViewBag.PageSize = categories.PageSize;
            ViewBag.PageCount = categories.TotalPages;
            ViewBag.TotalCount = categories.TotalCount;
            ViewBag.CurrentPage = categories.PageNumber;
            ViewBag.HasPreviousPage = categories.HasPreviousPage;
            ViewBag.HasNextPage = categories.HasNextPage;
            ViewBag.SearchString = searchString;

            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createdCategory = _categoryDataStore.CreateCategory(new Category { Name = name });

            if (createdCategory is null)
            {
                return BadRequest();
            }

            return RedirectToAction("Details", new { id = createdCategory.Id });
        }

        public IActionResult Details(int id)
        {
            var category = _categoryDataStore.GetCategory(id);

            return View(category);
        }

        public IActionResult Upload()
        {
            ViewBag.FileUploaded = false;
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            if (file is null)
            {
                ViewBag.FileUploaded = false;
                return View();
            }

            var customers = DeserializeFile(file);

            ViewBag.Categories = customers;
            ViewBag.FileUploaded = true;

            return View();
        }

        public IActionResult Download()
        {
            var result = _categoryDataStore.GetExportFile();

            return File(result, "application/xls", "Categories.xls");
        }

        [HttpPost]
        public IActionResult Edit(int id, string name)
        {
            _categoryDataStore.UpdateCategory(new Category()
            {
                Id = id,
                Name = name
            });

            return RedirectToAction("Details", new { id });
        }

        public IActionResult Edit(int id)
        {
            var category = _categoryDataStore.GetCategory(id);
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryDataStore.GetCategory((int)id);

            if (category == null)
            {
                return NotFound(category);
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int id)
        {

            _categoryDataStore.DeleteCategory(id);

            return RedirectToAction(nameof(Index));
        }

        private static List<Category> DeserializeFile(IFormFile file)
        {
            List<Category> categories = new();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using var stream = new MemoryStream();
            file.CopyTo(stream);
            stream.Position = 0;
            using var reader = ExcelReaderFactory.CreateReader(stream);

            while (reader.Read())
            {
                categories.Add(new Category
                {
                    Name = reader.GetValue(0)?.ToString()
                });
            }

            return categories;
        }
    }
}
