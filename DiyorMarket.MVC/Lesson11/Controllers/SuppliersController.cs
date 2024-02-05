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

        public IActionResult Index(string? searchString)
        {
            var suppliers = _supplierDataStore.GetSuppliers(searchString);

            ViewBag.Suppliers = suppliers.Data;
			ViewBag.PageSize = suppliers.PageSize;
			ViewBag.PageCount = suppliers.TotalPages;
			ViewBag.CurrentPage = suppliers.PageNumber;
			ViewBag.SearchString = searchString;

			return View();
        }
    }
}
