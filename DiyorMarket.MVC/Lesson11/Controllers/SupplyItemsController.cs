using Lesson11.Stores.SupplyItems;
using Microsoft.AspNetCore.Mvc;

namespace Lesson11.Controllers
{
    public class SupplyItemsController : Controller
    {
        private readonly ISupplyItemDataStore _supplyItemDataStore;

        public SupplyItemsController(ISupplyItemDataStore supplyItemDataStore)
        {
            _supplyItemDataStore = supplyItemDataStore ?? throw new ArgumentNullException(nameof(supplyItemDataStore));
        }

        public IActionResult Index()
        {
            var result = _supplyItemDataStore.GetSupplyItems();

            if (result is null)
            {
                return NotFound();
            }

            ViewBag.SupplyItemsCount = result.Data?.Count();
            ViewBag.CurrentPage = result.PageNumber;
            ViewBag.PageSize = result.PageSize;
            ViewBag.HasNext = result.HasNextPage;
            ViewBag.HasPrevious = result.HasPreviousPage;
            ViewBag.TotalPages = result.TotalPages;

            return View(result.Data);
        }
    }
}
