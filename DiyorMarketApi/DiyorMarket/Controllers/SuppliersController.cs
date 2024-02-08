using DiyorMarket.Domain.DTOs.Supplier;
using DiyorMarket.Domain.Interfaces.Services;
using DiyorMarket.Domain.ResourceParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
