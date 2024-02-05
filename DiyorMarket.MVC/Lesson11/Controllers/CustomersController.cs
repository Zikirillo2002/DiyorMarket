using Lesson11.Stores.Customers;
using Microsoft.AspNetCore.Mvc;

namespace Lesson11.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerDataStore _customerDataStore;

        public CustomersController(ICustomerDataStore customerDataStore)
        {
            _customerDataStore = customerDataStore ?? throw new ArgumentNullException(nameof(customerDataStore));
        }

        public IActionResult Index(string? searchString)
        {
            var customers = _customerDataStore.GetCustomers(searchString);

            ViewBag.Customers = customers.Data;
			ViewBag.PageSize = customers.PageSize;
			ViewBag.PageCount = customers.TotalPages;
			ViewBag.CurrentPage = customers.PageNumber;
			ViewBag.SearchString = searchString;

			return View();
        }
    }
}
