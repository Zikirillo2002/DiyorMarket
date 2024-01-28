using Microsoft.AspNetCore.Mvc;

namespace Lesson11.Controllers
{
    public class SuppliesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
