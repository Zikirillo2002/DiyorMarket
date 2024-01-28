using Microsoft.AspNetCore.Mvc;

namespace Lesson11.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
