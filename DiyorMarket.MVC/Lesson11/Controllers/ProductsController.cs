using Lesson11.Stores.Categories;
using Lesson11.Stores.Products;
using Microsoft.AspNetCore.Mvc;

namespace Lesson11.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductDataStore _productDataStore;

        public ProductsController(IProductDataStore productDataStore)
        {
            _productDataStore = productDataStore ?? throw new ArgumentNullException(nameof(productDataStore));
        }

        public IActionResult Index()
        {
            var products = _productDataStore.GetProducts();

            ViewBag.Products = products.Data;

            return View();
        }
    }
}
