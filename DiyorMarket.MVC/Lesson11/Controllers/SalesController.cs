using ExcelDataReader;
using Lesson11.Models;
using Lesson11.Stores.Customers;
using Lesson11.Stores.Sales;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lesson11.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISaleDataStore _saleDataStore;
        private readonly ICustomerDataStore _customersDataStore;

        public SalesController(ISaleDataStore saleDataStore, ICustomerDataStore customersDataStore)
        {
            _saleDataStore = saleDataStore ?? throw new ArgumentNullException(nameof(saleDataStore));
            _customersDataStore = customersDataStore ?? throw new ArgumentNullException(nameof(customersDataStore)); ;
        }

        public IActionResult Index(string? searchString, int? customerId, DateTime? saleDate ,int pageNumber)
        {
            var sales = _saleDataStore.GetSales(searchString, customerId, pageNumber, saleDate);
            var customers = GetAllCustomers(searchString);

            foreach (var sale in sales.Data.ToList())
            {
                sale.Customer = customers.FirstOrDefault(x => x.Id == sale.CustomerId);
            }

            ViewBag.Sales = sales.Data;
            ViewBag.Customers = customers;
            ViewBag.PageSize = sales.PageSize;
            ViewBag.PageCount = sales.TotalPages;
            ViewBag.TotalCount = sales.TotalCount;
            ViewBag.CurrentPage = sales.PageNumber;
            ViewBag.HasPreviousPage = sales.HasPreviousPage;
            ViewBag.HasNextPage = sales.HasNextPage;
            ViewBag.SearchString = searchString;

            return View();
        }

        public IActionResult Create()
        {
            var customers = GetAllCustomers(null);
            ViewBag.Customers = new SelectList(customers, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Sale sale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _saleDataStore.CreateSale(new Sale
            {
                SaleDate = sale.SaleDate,
                CustomerId = sale.CustomerId
            });

            if (result is null)
            {
                return BadRequest();
            }

            return RedirectToAction("Details", new { id = result.Id });
        }

        public IActionResult Details(int id)
        {
            var sale = _saleDataStore.GetSale(id);
            sale.Customer = _customersDataStore.GetCustomer(sale.CustomerId);

            return View(sale);
        }

        private List<Customer> GetAllCustomers(string? searchString)
        {
            int number = 1;
            var customerResponse = _customersDataStore.GetCustomers(searchString, number);
            var customers = customerResponse.Data.ToList();


            while (customerResponse.HasNextPage)
            {
                customerResponse = _customersDataStore.GetCustomers(searchString, ++number);
                customers.AddRange(customerResponse.Data.ToList());
            }

            return customers;
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

            var sales = DeserializeFile(file);

            ViewBag.Sales = sales;
            ViewBag.FileUploaded = true;

            return View();
        }

        public IActionResult DownloadXML()
        {
            var result = _saleDataStore.GetExportFile();

            return File(result, "application/xls", "Sales.xls");
        }
        public IActionResult DownloadPDF()
        {
            var result = _saleDataStore.GetExportFile();

            return File(result, "application/pdf", "Sales.pdf");
        }


        public IActionResult Edit(int id)
        {
            var sale = _saleDataStore.GetSale(id);
            var customers = GetAllCustomers(null);
            ViewBag.Customers = customers;
            sale.Customer = customers.FirstOrDefault(x => x.Id == sale.CustomerId);
            return View(sale);
        }

        [HttpPost]
        public IActionResult Edit(int id, DateTime saleDate, int customerId, decimal totalDue)
        {
            if (customerId == 0)
            {
                var sale = _saleDataStore.GetSale(id);
                customerId = sale.Customer.Id?? customerId;
            }
            _saleDataStore.UpdateSale(new Sale
            {
                Id = id,
                SaleDate = saleDate,
                CustomerId = customerId,
                TotalDue = totalDue
            });

            return RedirectToAction("Details", new { id = id });
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = _saleDataStore.GetSale((int)id);

            if (sale == null)
            {
                return NotFound(sale);
            }

            sale.Customer = _customersDataStore.GetCustomer(sale.CustomerId);
            return View(sale);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            _saleDataStore.DeleteSale(id);
            return RedirectToAction(nameof(Index));
        }

        private static List<Sale> DeserializeFile(IFormFile file)
        {
            List<Sale> categories = new();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using var stream = new MemoryStream();
            file.CopyTo(stream);
            stream.Position = 0;
            using var reader = ExcelReaderFactory.CreateReader(stream);

            while (reader.Read())
            {
                categories.Add(new Sale
                {
                    TotalDue = Convert.ToDecimal(reader.GetValue(0)),
                    SaleDate = Convert.ToDateTime(reader.GetValue(1)),
                    CustomerId = Convert.ToInt32(reader.GetValue(2)),
                });
            }

            return categories;
        }
    }
}
