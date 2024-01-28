using Microsoft.AspNetCore.Mvc;

namespace Lesson11.Controllers
{
    public class SaleItemsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
