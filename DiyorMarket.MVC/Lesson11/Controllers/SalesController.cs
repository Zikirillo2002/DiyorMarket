using Lesson11.Stores.Sales;
using Microsoft.AspNetCore.Mvc;

namespace Lesson11.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISaleDataStore _saleDataStore;

        public SalesController(ISaleDataStore saleDataStore)
        {
            _saleDataStore = saleDataStore ?? throw new ArgumentNullException(nameof(saleDataStore));
        }

        public IActionResult Index()
        {
            var sales = _saleDataStore.GetSales();

            ViewBag.Sales = sales.Data;

            return View();
        }
    }
}
