using Lesson11.Models;
using Lesson11.Stores.Customers;
using Lesson11.Stores.Sales;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Index(string? searchString, int? customerId, int pageNumber, int prevCustomerId)
        {
            var sales = _saleDataStore.GetSales(searchString, customerId, pageNumber);
            var customers = GetAllCustomers(searchString);

            foreach(var sale in sales.Data.ToList())
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
            return View();
        }

        [HttpPost]
        public IActionResult Create(DateTime dateTime, int customerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _saleDataStore.CreateSale(new Sale
            {
                SaleDate = dateTime,
                CustomerId = customerId
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
        [HttpPost]
        public IActionResult Edit(int id, DateTime saleDate, int customerId, decimal totalDue)
        {
            _saleDataStore.UpdateSale(new Sale
            {
                SaleDate = saleDate,
                CustomerId = customerId,
                TotalDue = totalDue
            });

            return RedirectToAction("Details", new { id = id });
        }

        public IActionResult Edit(int id)
        {
            var sale = _saleDataStore.GetSale(id);
            return View(sale);
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
            return View(sale);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            _saleDataStore.DeleteSale(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
