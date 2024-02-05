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

        public IActionResult Index(string? searchString)
        {
            var sales = _saleDataStore.GetSales(searchString);

            ViewBag.Sales = sales.Data;
			ViewBag.PageSize = sales.PageSize;
			ViewBag.PageCount = sales.TotalPages;
			ViewBag.CurrentPage = sales.PageNumber;
			ViewBag.SearchString = searchString;

			return View();
        }

        //public IActionResult Create()
        //{
        //    var customers = _customersDataStore.GetCustomers();

        //    ViewBag.Customers = customers.Data;

        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Create(SaleViewModel saleToCreate)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }
        //    return View();
        //}
    }
}
