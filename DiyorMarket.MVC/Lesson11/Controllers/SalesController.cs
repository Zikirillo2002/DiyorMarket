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

        public IActionResult Index()
        {
            var sales = _saleDataStore.GetSales();

            ViewBag.Sales = sales.Data;

            return View();
        }

        public IActionResult Create()
        {
            var customers = _customersDataStore.GetCustomers();

            ViewBag.Customers = customers.Data;

            return View();
        }

        [HttpPost]
        public IActionResult Create(SaleViewModel saleToCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return View();
        }
    }
}
