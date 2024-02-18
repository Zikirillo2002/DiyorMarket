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

        public IActionResult Index(string? searchString, int pageNumber)
        {
            var suppliers = _supplierDataStore.GetSuppliers(searchString, pageNumber);

            ViewBag.Suppliers = suppliers.Data;
            ViewBag.PageSize = suppliers.PageSize;
            ViewBag.PageCount = suppliers.TotalPages;
            ViewBag.TotalCount = suppliers.TotalCount;
            ViewBag.CurrentPage = suppliers.PageNumber;
            ViewBag.HasPreviousPage = suppliers.HasPreviousPage;
            ViewBag.HasNextPage = suppliers.HasNextPage;
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
        [HttpPost]
        public IActionResult Edit(Supplier supplier)
        {
            _supplierDataStore.UpdateSupplier(new Supplier()
            {
                Id = supplier.Id,
                FirstName = supplier.FirstName,
                LastName = supplier.LastName,
                PhoneNumber = supplier.PhoneNumber,
                Company = supplier.Company
            });

            return RedirectToAction("Details", new { supplier.Id });
        }

        public IActionResult Edit(int id)
        {
            var supplier = _supplierDataStore.GetSupplier(id);
            return View(supplier);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = _supplierDataStore.GetSupplier((int)id);

            if (supplier == null)
            {
                return NotFound(supplier);
            }
            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]

        public IActionResult Delete(int id)
        {
            _supplierDataStore.DeleteSupplier(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
