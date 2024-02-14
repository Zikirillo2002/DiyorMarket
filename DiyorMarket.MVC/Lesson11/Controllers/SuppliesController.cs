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

        public IActionResult Index(string? searchString, int supplierId, int pageNumber)
        {
            var result = _supplyDataStore.GetSupplies(searchString, supplierId, pageNumber);
            var suplier = result.Data.Select(x => x.Supplier).ToList();

            ViewBag.Supplies = result.Data;
            ViewBag.PageSize = result.PageSize;
            ViewBag.PageCount = result.TotalPages;
            ViewBag.TotalCount = result.TotalCount;
            ViewBag.CurrentPage = result.PageNumber;
            ViewBag.HasPreviousPage = result.HasPreviousPage;
            ViewBag.HasNextPage = result.HasNextPage;
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

        [HttpPost]
        public IActionResult Edit(int id, DateTime supplyDate, int supplierId)
        {
            _supplyDataStore.UpdateSupply(new Supply
            {
                Id = id,
                SupplyDate = supplyDate,
                SupplierId = supplierId,
            });

            return RedirectToAction("Details", new { id = id });
        }
        public IActionResult Edit(int id)
        {
            var supply = _supplyDataStore.GetSupply(id);
            return View(supply);
        }

        public IActionResult Delete(int id)
        {
            _supplyDataStore.DeleteSupply(id);

            return RedirectToAction("Index");
        }
    }
}
