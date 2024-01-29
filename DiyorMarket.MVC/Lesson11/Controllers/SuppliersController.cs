using Lesson11.Stores.Suppliers;
using Microsoft.AspNetCore.Mvc;

namespace Lesson11.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly ISupplierDataStore _supplierDataStore;

        public SuppliersController(ISupplierDataStore supplierDataStore)
        {
            _supplierDataStore = supplierDataStore ?? throw new ArgumentNullException(nameof(supplierDataStore));
        }

        public IActionResult Index()
        {
            var result = _supplierDataStore.GetSuppliers();

            if (result is null)
            {
                return NotFound();
            }

            this.SetViewBagProperties(result);
            ViewBag.SuppliersCount = result.Data?.Count();
            ViewBag.CurrentPage = result.PageNumber;
            ViewBag.PageSize = result.PageSize;
            ViewBag.HasNext = result.HasNextPage;
            ViewBag.HasPrevious = result.HasPreviousPage;
            ViewBag.TotalPages = result.TotalPages;

            return View(result.Data);
        }
    }
}
