using Lesson11.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lesson11.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            return statusCode switch
            {
                401 => RedirectToAction("Index", "Auth"),
                403 => RedirectToAction(nameof(Forbidden)),
                404 => RedirectToAction(nameof(NotFound)),
                _ => RedirectToAction(nameof(InternalServerError)),
            };
        }

        public IActionResult Forbidden()
        {
            return View();
        }

        public IActionResult NotFound()
        {
            return View();
        }

        public IActionResult InternalServerError()
        {
            return View();
        }
    }
}