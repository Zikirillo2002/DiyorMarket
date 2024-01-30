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

        public IActionResult Index()
        {
            var result = _customerDataStore.GetCustomers();

            if (result is null)
            {
                return NotFound();
            }

            this.SetViewBagProperties(result);

            return View(result.Data);
        }
    }
}
