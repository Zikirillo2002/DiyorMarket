using Lesson11.Models;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _supplierDataStore.CreateSupplier(new Supplier
            {
                FirstName = supplier.FirstName,
                LastName = supplier.LastName,
                PhoneNumber = supplier.PhoneNumber,
                Company = supplier.Company  
            });

            if (result is null)
            {
                return BadRequest();
            }

            return RedirectToAction("Details", new { id = result.Id });
        }

        public IActionResult Details(int id)
        {
            var supplier = _supplierDataStore.GetSupplier(id);

            return View(supplier);
        }
    }
}
