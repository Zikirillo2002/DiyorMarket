﻿using DiyorMarket.Domain.DTOs.Category;
using DiyorMarket.Domain.DTOs.Product;
using DiyorMarket.Domain.Interfaces.Services;
using DiyorMarket.Domain.ResourceParameters;
using DiyorMarket.ResourceParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<CategoryDto>> GetCategoriesAsync(
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
    }
}
