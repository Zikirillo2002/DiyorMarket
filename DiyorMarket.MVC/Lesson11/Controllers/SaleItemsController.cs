using Lesson11.Stores.SaleItems;
using Microsoft.AspNetCore.Mvc;

namespace Lesson11.Controllers
{
    public class SaleItemsController : Controller
    {
        private readonly ISaleItemDataStore _saleItemDataStore;

        public SaleItemsController(ISaleItemDataStore saleItemDataStore)
        {
            _saleItemDataStore = saleItemDataStore ?? throw new ArgumentNullException(nameof(saleItemDataStore));
        }

        public IActionResult Index()
        {
            var result = _saleItemDataStore.GetSaleItems();

            if (result is null)
            {
                return NotFound();
            }

            this.SetViewBagProperties(result);

            return View(result.Data);
        }
    }
}
