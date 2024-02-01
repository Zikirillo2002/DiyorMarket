using Lesson11.Stores;
using Lesson11.Stores.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lesson11.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ICommonDataStore _commonDataStore;

        public ProductsController(ICommonDataStore commonDataStore)
        {
            _commonDataStore = commonDataStore ?? throw new ArgumentNullException(nameof(productDataStore));
        }

        public IActionResult Index()
        {
            var products = _commonDataStore.Products.GetProducts();

            ViewBag.Products = products.Data;

            return View();
        }
    }
}
