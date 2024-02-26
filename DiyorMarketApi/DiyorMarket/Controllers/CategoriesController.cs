using ClosedXML.Excel;
using DiyorMarket.Domain.DTOs.Category;
using DiyorMarket.Domain.DTOs.Customer;
using DiyorMarket.Domain.DTOs.Product;
using DiyorMarket.Domain.Interfaces.Services;
using DiyorMarket.Domain.ResourceParameters;
using DiyorMarket.ResourceParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DiyorMarketApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    //[Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CategoriesController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> GetCategories(
            [FromQuery] CategoryResourceParameters categoryResourceParameters)
        {
            var categories = _categoryService.GetCategories(categoryResourceParameters);

            return Ok(categories);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public ActionResult<CategoryDto> Get(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            return Ok(category);
        }

        //[HttpGet("export")]
        //public ActionResult ExportCustomers()
        //{
        //    var category = _categoryService.GetAllCategories();

        //    using XLWorkbook wb = new XLWorkbook();
        //    var sheet1 = wb.AddWorksheet(GetCategoriesDataTable(category), "Categories");

        //    sheet1.Column(1).Style.Font.FontColor = XLColor.Red;

        //    sheet1.Columns(2, 4).Style.Font.FontColor = XLColor.Blue;

        //    sheet1.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.Black;
        //    //sheet1.Row(1).Cells(1,3).Style.Fill.BackgroundColor = XLColor.Yellow;
        //    sheet1.Row(1).Style.Font.FontColor = XLColor.White;

        //    sheet1.Row(1).Style.Font.Bold = true;
        //    sheet1.Row(1).Style.Font.Shadow = true;
        //    sheet1.Row(1).Style.Font.Underline = XLFontUnderlineValues.Single;
        //    sheet1.Row(1).Style.Font.VerticalAlignment = XLFontVerticalTextAlignmentValues.Superscript;
        //    sheet1.Row(1).Style.Font.Italic = true;

        //    sheet1.Rows(2, 3).Style.Font.FontColor = XLColor.AshGrey;

        //    using MemoryStream ms = new MemoryStream();
        //    wb.SaveAs(ms);
        //    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Categories.xlsx");
        //}

        [HttpGet("{id}/products")]
        public ActionResult<ProductDto> GetProductsByCategoryId(
            int id,
            ProductResourceParameters productResourceParameters)
        {
            var products = _productService.GetProducts(productResourceParameters);

            return Ok(products);
        }

        [HttpPost]
        public ActionResult Post([FromBody] CategoryForCreateDto category)
        {
            var createdCategory = _categoryService.CreateCategory(category);

            return CreatedAtAction(nameof(Get), new { createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CategoryForUpdateDto category)
        {
            if (id != category.Id)
            {
                return BadRequest(
                    $"Route id: {id} does not match with parameter id: {category.Id}.");
            }

            var updatedCategory = _categoryService.UpdateCategory(category);

            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);

            return NoContent();
        }

        //private DataTable GetCategoriesDataTable(IEnumerable<CategoryDto> categories)
        //{
        //    DataTable table = new DataTable();
        //    table.TableName = "Categories Data";
        //    table.Columns.Add("Id", typeof(int));
        //    table.Columns.Add("Name", typeof(string));
        //    table.Columns.Add("NumberOfProducts", typeof(string));

        //    foreach (var category in categories)
        //    {
        //        table.Rows.Add(category.Id, category.Name);
        //    }

        //    return table;
        //}
    }
}
