using Lesson11.Models;
using Lesson11.Stores.Customers;
using Lesson11.Stores.Sales;
using Lesson11.ViewModels;
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

        public IActionResult Index(string? searchString, int pageNumber)
        {
            var sales = _saleDataStore.GetSales(searchString,pageNumber);
            var customers = GetAllCustomers(searchString);

            foreach(var sale in sales.Data.ToList())
            {
                sale.Customer = customers.FirstOrDefault(x => x.Id == sale.CustomerId);
            }
            

            ViewBag.Sales = sales.Data;
            ViewBag.Customers = customers;
			ViewBag.PageSize = sales.PageSize;
			ViewBag.PageCount = sales.TotalPages;
			ViewBag.CurrentPage = sales.PageNumber;
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
    }
}
