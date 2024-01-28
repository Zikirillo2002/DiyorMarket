using Microsoft.AspNetCore.Mvc;

namespace Lesson11.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
