using Microsoft.AspNetCore.Mvc;

namespace Lesson11.Controllers
{
    public class DashboardController : Controller
    {
        

        public DashboardController()
        {
            
        }

        public IActionResult Index()
        {
            
            return View();
        }
    }
}
