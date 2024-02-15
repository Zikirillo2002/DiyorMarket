using Lesson11.Models;
using Lesson11.Stores.Categories;
using Lesson11.Stores.Products;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.EJ2.Linq;

namespace Lesson11.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductDataStore _productDataStore;
        private readonly ICategoryDataStore _categoryDataStore;

        public ProductsController(IProductDataStore productDataStore, ICategoryDataStore categoryDataStore)
        {
            _productDataStore = productDataStore ?? throw new ArgumentNullException(nameof(productDataStore));
            _categoryDataStore = categoryDataStore ?? throw new ArgumentNullException(nameof(categoryDataStore));
        }

        public IActionResult Index(string? searchString, int? categoryId, int pageNumber, int? prevCategoryId)
        {
            if (categoryId == null && prevCategoryId != null)
            {
                categoryId = prevCategoryId;
            }

            var filteredProducts = _productDataStore.GetProducts(searchString, categoryId, pageNumber);

            var categories = GetAllCategories();

            categories.Insert(0, new Category
            {
                Id = 0,
                Name = "All"
            });

            var selectCategory = categories[0];

            if (categoryId.HasValue && categoryId != 0)
            {
                selectCategory = categories.FirstOrDefault(x => x.Id == categoryId);
            }

            ViewBag.Products = filteredProducts.Data;
            ViewBag.SelectedCategory = selectCategory;

            ViewBag.Categories = categories;
            ViewBag.PageSize = filteredProducts.PageSize;
            ViewBag.PageCount = filteredProducts.TotalPages;
            ViewBag.TotalCount = filteredProducts.TotalCount;
            ViewBag.CurrentPage = filteredProducts.PageNumber;
            ViewBag.HasPreviousPage = filteredProducts.HasPreviousPage;
            ViewBag.HasNextPage = filteredProducts.HasNextPage;
            ViewBag.CurrentCategoryId = categoryId;
            ViewBag.SearchString = searchString;

            return View();
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = _productDataStore.CreateProduct(new Product
            {
                Name = product.Name,
                Description = product.Description,
                SalePrice = product.SalePrice,
                SupplyPrice = product.SupplyPrice,
				ExpireDate = product.ExpireDate,
				CategoryId = product.CategoryId 
            });

			if (result is null)
			{
				return BadRequest();
			}

			return RedirectToAction("Details", new { id = result.Id});
		}

        public IActionResult Details(int id)
        {
            var product = _productDataStore.GetProduct(id);

			return View(product);
        }

        private List<Category> GetAllCategories()
        {
            int number = 1;
            var categoryResponse = _categoryDataStore.GetCategories(null, number);
            var categories = categoryResponse.Data.ToList();


            for(int i = 1; i <= categoryResponse.TotalPages; i++)
            {
                categoryResponse = _categoryDataStore.GetCategories(null, ++number);
                categories.AddRange(categoryResponse.Data.ToList());
            }

            return categories;
        }

    }
}
