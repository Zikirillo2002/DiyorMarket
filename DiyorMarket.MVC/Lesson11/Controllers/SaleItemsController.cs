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

            ViewBag.SaleItemsCount = result.Data?.Count();
            ViewBag.CurrentPage = result.PageNumber;
            ViewBag.PageSize = result.PageSize;
            ViewBag.HasNext = result.HasNextPage;
            ViewBag.HasPrevious = result.HasPreviousPage;
            ViewBag.TotalPages = result.TotalPages;

            return View(result.Data);
        }
    }
}
