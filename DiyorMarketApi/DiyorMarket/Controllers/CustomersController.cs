using ClosedXML.Excel;
using DiyorMarket.Domain.DTOs.Customer;
using DiyorMarket.Domain.Interfaces.Services;
using DiyorMarket.Domain.ResourceParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Data;
using System.Drawing;

namespace DiyorMarket.Controllers
{
    [Route("api/customers")]
    [ApiController]
    //[Authorize]
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

        [HttpGet("export")]
        public ActionResult ExportCustomers()
        {
            var customers = _customerService.GetCustomers();

            using XLWorkbook wb = new XLWorkbook();
            var sheet1 = wb.AddWorksheet(GetCustomersDataTable(customers), "Customers");

            sheet1.Column(1).Style.Font.FontColor = XLColor.Red;

            sheet1.Columns(2, 4).Style.Font.FontColor = XLColor.Blue;

            sheet1.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.Black;
            //sheet1.Row(1).Cells(1,3).Style.Fill.BackgroundColor = XLColor.Yellow;
            sheet1.Row(1).Style.Font.FontColor = XLColor.White;

            sheet1.Row(1).Style.Font.Bold = true;
            sheet1.Row(1).Style.Font.Shadow = true;
            sheet1.Row(1).Style.Font.Underline = XLFontUnderlineValues.Single;
            sheet1.Row(1).Style.Font.VerticalAlignment = XLFontVerticalTextAlignmentValues.Superscript;
            sheet1.Row(1).Style.Font.Italic = true;

            sheet1.Rows(2, 3).Style.Font.FontColor = XLColor.AshGrey;

            using MemoryStream ms = new MemoryStream();
            wb.SaveAs(ms);
            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Customers.xlsx");
        }

        [HttpPost]
        public ActionResult Post([FromBody] CustomerForCreateDto customer)
        {
            var createCustomer = _customerService.CreateCustomer(customer);

            return CreatedAtAction(nameof(Get), new { createCustomer.Id }, createCustomer);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CustomerForUpdateDto customer)
        {
            if (id != customer.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {customer.Id}.");
            }

            var updateCastomer = _customerService.UpdateCustomer(customer);

            return Ok(updateCastomer);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _customerService.DeleteCustomer(id);

            return NoContent();
        }

        private DataTable GetCustomersDataTable(IEnumerable<CustomerDto> customers)
        {
            DataTable table = new DataTable();
            table.TableName = "Customers Data";
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Phone", typeof(string));

            foreach(var customer in customers)
            {
                table.Rows.Add(customer.Id, customer.FullName, customer.PhoneNumber);
            }

            return table;
        }
    }
}
