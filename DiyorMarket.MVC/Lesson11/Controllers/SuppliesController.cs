using Lesson11.Stores.Supplies;
using Microsoft.AspNetCore.Mvc;

namespace Lesson11.Controllers
{
    public class SuppliesController : Controller
    {
        private readonly ISupplyDataStore _supplyDataStore;

        public SuppliesController(ISupplyDataStore supplyDataStore)
        {
            _supplyDataStore = supplyDataStore ?? throw new ArgumentNullException(nameof(supplyDataStore));
        }

        public IActionResult Index()
        {
            var result = _supplyDataStore.GetSupplies();

            if (result is null)
            {
                return NotFound();
            }

            ViewBag.SuppliesCount = result.Data?.Count();
            ViewBag.CurrentPage = result.PageNumber;
            ViewBag.PageSize = result.PageSize;
            ViewBag.HasNext = result.HasNextPage;
            ViewBag.HasPrevious = result.HasPreviousPage;
            ViewBag.TotalPages = result.TotalPages;

            return View(result.Data);
        }
    }
}
