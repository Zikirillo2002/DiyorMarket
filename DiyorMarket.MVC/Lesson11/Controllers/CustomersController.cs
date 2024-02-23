using ExcelDataReader;
using Lesson11.Models;
using Lesson11.Stores.Customers;
using Lesson11.Stores.SaleItems;
using Lesson11.Stores.Sales;
using Lesson11.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lesson11.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerDataStore _customerDataStore;
        private readonly ISaleDataStore _saleDataStore;
        private readonly ISaleItemDataStore _saleItemDataStore;

        public CustomersController(ICustomerDataStore customerDataStore, ISaleDataStore saleDataStore, ISaleItemDataStore saleItemDataStore)
        {
            _customerDataStore = customerDataStore ?? throw new ArgumentNullException(nameof(customerDataStore));
            _saleDataStore = saleDataStore ?? throw new ArgumentNullException(nameof(saleDataStore));
            _saleItemDataStore = saleItemDataStore ?? throw new ArgumentNullException(nameof(saleItemDataStore));
        }

        public IActionResult Index(string? searchString, int pageNumber)
        {
            var customers = _customerDataStore.GetCustomers(searchString, pageNumber);

            ViewBag.Customers = customers.Data;
            ViewBag.PageSize = customers.PageSize;
            ViewBag.PageCount = customers.TotalPages;
            ViewBag.TotalCount = customers.TotalCount;
            ViewBag.CurrentPage = customers.PageNumber;
            ViewBag.HasPreviousPage = customers.HasPreviousPage;
            ViewBag.HasNextPage = customers.HasNextPage;
            ViewBag.SearchString = searchString;

            return View();
        }

        public IActionResult Details(int id)
        {
            var customersSales = _saleDataStore.GetCustomersSale(id);
            var customer = _customerDataStore.GetCustomer(id);

            ViewBag.CustomersSale = customersSales;

            return View(customer);
        }

        public IActionResult GetSalesSaleItems(int id)
        {
            var salesSaleItems = _saleItemDataStore.GetSalesSaleItems(id);
            var customersSaleItems = new List<CustomerSaleItemsViewModels>();
            var sale = _saleDataStore.GetSale(id);

            foreach(var item in salesSaleItems)
            {
                customersSaleItems.Add(new CustomerSaleItemsViewModels()
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    ProductId = item.ProductId,
                    Product = item.Product,
                    SaleId = item.SaleId,
                    Sale = sale,
                    TotalPrice = item.Quantity * item.UnitPrice
                });
            }

            return View(customersSaleItems);
        }

        public IActionResult Create()
        {
            return View();
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

            ViewBag.Customers = customers;
            ViewBag.FileUploaded = true;

            return View();
        }

        [HttpPost]
        public IActionResult Create(string? firstName, string? lastName, string? phoneNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var fullName = $"{firstName} {lastName}";

            var result = _customerDataStore.CreateCustomer(new Customer
            {
                FullName = fullName,
                PhoneNumber = phoneNumber,
            });

            if (result is null)
            {
                return BadRequest();
            }

            return RedirectToAction("Details", new { id = result.Id });
        }

        public IActionResult Download()
        {
            var result = _customerDataStore.GetExportFile();

            return File(result, "application/xls", "Customers.xls");
        }

        public IActionResult Edit(int id)
        {
            var customer = _customerDataStore.GetCustomer(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(string? firstName, string? lastName, string? phoneNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var fullName = $"{firstName} {lastName}";

            var result = _customerDataStore.UpdateCustomer(new Customer
            {
                FullName = fullName,
                PhoneNumber = phoneNumber,
            });

            if (result is null)
            {
                return BadRequest();
            }

            return RedirectToAction("Details", new { id = result.Id });
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _customerDataStore.GetCustomer((int)id);

            if (customer == null)
            {
                return NotFound(customer);
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int id)
        {

            _customerDataStore.DeleteCustomer(id);

            return RedirectToAction(nameof(Index));
        }

        private static List<Customer> DeserializeFile(IFormFile file)
        {
            List<Customer> customers = new();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using var stream = new MemoryStream();
            file.CopyTo(stream);
            stream.Position = 0;
            using var reader = ExcelReaderFactory.CreateReader(stream);

            while (reader.Read())
            {
                customers.Add(new Customer
                {
                    FullName = reader.GetValue(0)?.ToString(),
                    PhoneNumber = reader.GetValue(1)?.ToString()
                });
            }

            return customers;
        }
    }
}
