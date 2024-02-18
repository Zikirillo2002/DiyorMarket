using ClosedXML.Excel;
using DiyorMarket.Domain.DTOs.Product;
using DiyorMarket.Domain.DTOs.Supplier;
using DiyorMarket.Domain.Interfaces.Services;
using DiyorMarket.Domain.ResourceParameters;
using DiyorMarket.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DiyorMarket.Controllers
{
    [Route("api/suppliers")]
    [ApiController]
    //[Authorize]
    public class SuppliersController : Controller
    {
        private readonly ISupplierService _supplierService;
        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SupplierDto>> GetSuppliersAsync(
            [FromQuery] SupplierResourceParameters supplierResourceParameters)
        {
            var suppliers = _supplierService.GetSuppliers(supplierResourceParameters);

            return Ok(suppliers);
        }

        [HttpGet("export")]
        public ActionResult ExportSuppliers()
        {
            var category = _supplierService.GetAllSuppliers();

            using XLWorkbook wb = new XLWorkbook();
            var sheet1 = wb.AddWorksheet(GetSuppliersTable(category), "Suppliers");

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
            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Suppliers.xlsx");
        }

        [HttpGet("{id}", Name = "GetSupplierById")]
        public ActionResult<SupplierDto> Get(int id)
        {
            var supplier = _supplierService.GetSupplierById(id);

            if (supplier is null)
            {
                return NotFound($"Supplier with id: {id} does not exist.");
            }

            return Ok(supplier);
        }

        [HttpPost]
        public ActionResult Post([FromBody] SupplierForCreateDto supplier)
        {
            var createSupplier = _supplierService.CreateSupplier(supplier);

            return CreatedAtAction(nameof(Get), new { createSupplier.Id }, createSupplier);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SupplierForUpdateDto supplier)
        {
            if (id != supplier.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {supplier.Id}.");
            }

            var updatedSupplier = _supplierService.UpdateSupplier(supplier);

            return Ok(updatedSupplier);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _supplierService.DeleteSupplier(id);

            return NoContent();
        }

        private DataTable GetSuppliersTable(IEnumerable<SupplierDto> supplierDtos)
        {
            DataTable table = new DataTable();
            table.TableName = "Suppliers Data";
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("FirstName", typeof(string));
            table.Columns.Add("LastName", typeof(string));
            table.Columns.Add("PhoneNumber", typeof(string));
            table.Columns.Add("Company", typeof(string));

            foreach (var supplier in supplierDtos)
            {
                table.Rows.Add(supplier.Id,
                    supplier.FirstName,
                    supplier.LastName,
                    supplier.PhoneNumber,
                    supplier.Company);
            }

            return table;
        }
    }
}
