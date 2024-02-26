using ExcelDataReader;
using Lesson11.Models;
using Lesson11.Stores.Suppliers;
using Lesson11.Stores.Supplies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lesson11.Controllers
{
    public class SuppliesController : Controller
    {
        private readonly ISupplyDataStore _supplyDataStore;
        private readonly ISupplierDataStore _supplierDataStore;

        public SuppliesController(ISupplyDataStore supplyDataStore,
            ISupplierDataStore supplierDataStore)
        {
            _supplyDataStore = supplyDataStore ?? throw new ArgumentNullException(nameof(supplyDataStore));
            _supplierDataStore = supplierDataStore ?? throw new ArgumentNullException(nameof(supplierDataStore));
        }

        public IActionResult Index(string? searchString, int? supplierId, int pageNumber, int? prevSupplierId)
        {
            if (supplierId == null && prevSupplierId != null)
            {
                supplierId = prevSupplierId;
            }

            var result = _supplyDataStore.GetSupplies(searchString, supplierId, pageNumber);
            var supliers = GetAllSupplier(searchString);

            supliers.Insert(0, new Supplier
            {
                Id = 0,
                FirstName = "All"
            });

            var selectSuppliers = supliers[0];
            if (supplierId.HasValue && supplierId != 0)
            {
                selectSuppliers = supliers.FirstOrDefault(x => x.Id == supplierId);
            }

            foreach (var supply in result.Data.ToList())
            {
                supply.Supplier = supliers.FirstOrDefault(x => x.Id == supply.SupplierId);
            }

            ViewBag.SelectedSupplier = selectSuppliers;

            ViewBag.Supplies = result.Data;
            ViewBag.Suppliers = supliers;
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
            var suppliers = GetAllSupplier(null);
            ViewBag.Suppliers = new SelectList(suppliers, "Id", "FirstName");
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
            var supply = _supplyDataStore.GetSupply(id);
            supply.Supplier = _supplierDataStore.GetSupplier(supply.SupplierId);

            return View(supply);
        }

        [HttpPost]
        public IActionResult Edit(int id, DateTime supplyDate, int supplierId)
        {
            if (supplierId == 0)
            {
                var supply = _supplyDataStore.GetSupply(id);
                supplierId = supply.Supplier.Id;
            }
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
            var suppliers = GetAllSupplier(null);
            ViewBag.Suppliers = suppliers;
            supply.Supplier = suppliers.FirstOrDefault(x => x.Id == supply.SupplierId);
            return View(supply);
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
            var result = _supplyDataStore.GetExportFile();

            return File(result, "application/xls", "Supplies.xls");
        }
        private List<Supplier> GetAllSuppliers(string? searchString)
        {
            int number = 1;
            var supplierResponse = _supplierDataStore.GetSuppliers(searchString, number);
            var suppliers = supplierResponse.Data.ToList();


            while (supplierResponse.HasNextPage)
            {
                supplierResponse = _supplierDataStore.GetSuppliers(searchString, ++number);
                suppliers.AddRange(supplierResponse.Data.ToList());
            }

            return suppliers;
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supply = _supplyDataStore.GetSupply((int)id);

            if (supply == null)
            {
                return NotFound(supply);
            }
            return View(supply);
        }

        [HttpPost, ActionName("Delete")]

        public IActionResult Delete(int id)
        {
            _supplyDataStore.DeleteSupply(id);

            return RedirectToAction("Index");
        }

        private static List<Supply> DeserializeFile(IFormFile file)
        {
            List<Supply> supply = new();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using var stream = new MemoryStream();
            file.CopyTo(stream);
            stream.Position = 0;
            using var reader = ExcelReaderFactory.CreateReader(stream);

            while (reader.Read())
            {
                supply.Add(new Supply
                {
                    SupplyDate = Convert.ToDateTime(reader.GetValue(0)),
                    TotalDue = Convert.ToDecimal(reader.GetValue(1)),
                    SupplierId = Convert.ToInt32(reader.GetValue(2)),
                });
            }

            return supply;
        }

        private List<Supplier> GetAllSupplier(string? searchString)
        {
            int number = 1;
            var categoryResponse = _supplierDataStore.GetSuppliers(searchString, number);
            var categories = categoryResponse.Data.ToList();


            for (int i = 1; i <= categoryResponse.TotalPages; i++)
            {
                categoryResponse = _supplierDataStore.GetSuppliers(null, ++number);
                categories.AddRange(categoryResponse.Data.ToList());
            }

            return categories;
        }
    }
}
