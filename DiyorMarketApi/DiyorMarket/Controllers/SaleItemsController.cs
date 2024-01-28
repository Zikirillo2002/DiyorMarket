using DiyorMarket.Domain.DTOs.SaleItem;
using DiyorMarket.Domain.Interfaces.Services;
using DiyorMarket.Domain.Pagniation;
using DiyorMarket.Domain.ResourceParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DiyorMarket.Controllers
{
    [Route("api/saleItems")]
    [ApiController]
    [Authorize]
    public class SaleItemsController : Controller
    {
        private readonly ISaleItemService _saleItemService;
        public SaleItemsController(ISaleItemService saleItemService)
        {
            _saleItemService = saleItemService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SaleItemDto>> GetSaleItemsAsync(
            [FromQuery] SaleItemResourceParameters saleItemResourceParameters)
        {
            var saleItems = _saleItemService.GetSaleItems(saleItemResourceParameters);

            return Ok(saleItems);
        }

        [HttpGet("{id}", Name = "GetSaleItemById")]
        public ActionResult<SaleItemDto> Get(int id)
        {
            var saleItem = _saleItemService.GetSaleItemById(id);

            if (saleItem is null)
            {
                return NotFound($"SaleItem with id: {id} does not exist.");
            }

            return Ok(saleItem);
        }

        [HttpPost]
        public ActionResult Post([FromBody] SaleItemForCreateDto saleItem)
        {
            _saleItemService.CreateSaleItem(saleItem);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SaleItemForUpdateDto saleItem)
        {
            if (id != saleItem.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {saleItem.Id}.");
            }

            _saleItemService.UpdateSaleItem(saleItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _saleItemService.DeleteSaleItem(id);

            return NoContent();
        }
    }
}
