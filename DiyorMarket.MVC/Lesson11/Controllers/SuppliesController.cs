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

        public IActionResult Index(string? searchString)
        {
            var result = _supplyDataStore.GetSupplies(searchString);

            ViewBag.Supplies = result.Data;
			ViewBag.PageSize = result.PageSize;
			ViewBag.PageCount = result.TotalPages;
			ViewBag.CurrentPage = result.PageNumber;
			ViewBag.SearchString = searchString;

			return View(result.Data);
        }
    }
}
