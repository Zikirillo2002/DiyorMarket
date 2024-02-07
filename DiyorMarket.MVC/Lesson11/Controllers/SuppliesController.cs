using Lesson11.Models;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Supply supply)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _supplyDataStore.CreateSupply(new Supply
            {
                SupplyDate = supply.SupplyDate,
                SupplierId = supply.SupplierId,
            });

            if (result is null)
            {
                return BadRequest();
            }

            return RedirectToAction("Details", new { id = result.Id });
        }

        public IActionResult Details(int id)
        {
            var createSupply = _supplyDataStore.GetSupply(id);

            return View(createSupply);
        }
    }
}
