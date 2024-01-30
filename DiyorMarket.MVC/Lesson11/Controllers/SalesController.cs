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
            var result = _saleDataStore.GetSales();

            if (result is null)
            {
                return NotFound();
            }

            this.SetViewBagProperties(result);

            return View(result.Data);
        }
    }
}
