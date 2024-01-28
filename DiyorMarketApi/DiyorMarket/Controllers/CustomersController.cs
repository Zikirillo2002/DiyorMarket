using DiyorMarket.Domain.DTOs.Category;
using DiyorMarket.Domain.DTOs.Customer;
using DiyorMarket.Domain.DTOs.Product;
using DiyorMarket.Domain.Entities;
using DiyorMarket.Domain.Interfaces.Services;
using DiyorMarket.Domain.Pagniation;
using DiyorMarket.Domain.ResourceParameters;
using DiyorMarket.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DiyorMarket.Controllers
{
    [Route("api/customers")]
    [ApiController]
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerDto>> GetCustomersAsync(
            [FromQuery] CustomerResourceParameters customerResourceParameters)
        {
            var customers = _customerService.GetCustomers(customerResourceParameters);

            return Ok(customers);
        }

        [HttpGet("{id}", Name = "GetCustomerById")]
        public ActionResult<CustomerDto> Get(int id)
        {
            var customer = _customerService.GetCustomerById(id);

            if (customer is null)
            {
                return NotFound($"Customer with id: {id} does not exist.");
            }

            return Ok(customer);
        }

        [HttpPost]
        public ActionResult Post([FromBody] CustomerForCreateDto customer)
        {
            _customerService.CreateCustomer(customer);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CustomerForUpdateDto customer)
        {
            if (id != customer.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {customer.Id}.");
            }

            _customerService.UpdateCustomer(customer);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _customerService.DeleteCustomer(id);

            return NoContent();
        }

        private PaginationMetaData GetPaginationMetaData(PaginatedList<CustomerDto> customers)
        {
            return new PaginationMetaData
            {
                TotalCount = customers.TotalCount,
                PageSize = customers.PageSize,
                CurrentPage = customers.CurrentPage,
                TotalPages = customers.TotalPages,
            };
        }
    }
}
