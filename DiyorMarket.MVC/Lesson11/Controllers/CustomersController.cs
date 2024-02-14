using Lesson11.Models;
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

        public IActionResult Index(string? searchString, int pageNumber)
        {
            var customers = _customerDataStore.GetCustomers(searchString,pageNumber);

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
            var customer = _customerDataStore.GetCustomer(id);

            return View(customer);
        }

        public IActionResult Create()
        {
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

        public IActionResult Edit(int id)
        {
            var customer = _customerDataStore.GetCustomer(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit( string? firstName, string? lastName, string? phoneNumber)
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

        public IActionResult Delete(int id)
        {
            _customerDataStore.DeleteCustomer(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
