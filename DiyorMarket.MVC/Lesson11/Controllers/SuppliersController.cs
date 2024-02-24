using ExcelDataReader;
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

        public IActionResult Upload()
        {
            ViewBag.FileUploaded = false;
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            if (file is null)
            {
                ViewBag.FileUploaded = false;
                return View();
            }

            var customers = DeserializeFile(file);

            ViewBag.Categories = customers;
            ViewBag.FileUploaded = true;

            return View();
        }

        public IActionResult Download()
        {
            var result = _supplierDataStore.GetExportFile();

            return File(result, "application/xls", "Suppliers.xls");
        }

        [HttpPost]
        public IActionResult Edit(int id, string? firstName, string? lastName, string? phoneNumber, string? company)
        {
            _supplierDataStore.UpdateSupplier(new Supplier()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Company = company
            });

            return RedirectToAction("Details", new { id });
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

        private static List<Supplier> DeserializeFile(IFormFile file)
        {
            List<Supplier> suppliers = new();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using var stream = new MemoryStream();
            file.CopyTo(stream);
            stream.Position = 0;
            using var reader = ExcelReaderFactory.CreateReader(stream);

            while (reader.Read())
            {
                suppliers.Add(new Supplier
                {
                    FirstName = reader.GetValue(0)?.ToString(),
                    LastName = reader.GetValue(1)?.ToString(),
                    PhoneNumber = reader.GetValue(2)?.ToString(),
                    Company = reader.GetValue(3)?.ToString()    
                });
            }

            return suppliers;
        }
    }
}
