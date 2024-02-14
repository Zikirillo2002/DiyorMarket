using Lesson11.Models;
using Lesson11.Stores.Categories;
using Lesson11.Stores.Products;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.EJ2.Linq;

namespace Lesson11.Controllers;

public class ProductsController : Controller
{
    private readonly IProductDataStore _productDataStore;
    private readonly ICategoryDataStore _categoryDataStore;

    public ProductsController(IProductDataStore productDataStore, ICategoryDataStore categoryDataStore)
    {
        _productDataStore = productDataStore ?? throw new ArgumentNullException(nameof(productDataStore));
        _categoryDataStore = categoryDataStore ?? throw new ArgumentNullException(nameof(categoryDataStore));
    }

    public IActionResult Index(string? searchString, int? categoryId, int pageNumber)
    {
        var products = _productDataStore.GetProducts(searchString, categoryId, pageNumber);
        var categories = _categoryDataStore.GetCategories(searchString, 1).Data.ToList();
        categories.Insert(0, new Category
        {
            Id = 0,
            Name = "All"
        });

        ViewBag.Categories = categories;
        ViewBag.SelectedCategory = categoryId ?? categories[0].Id;
        ViewBag.Products = products.Data;
        ViewBag.PageSize = products.PageSize;
        ViewBag.PageCount = products.TotalPages;
        ViewBag.TotalCount = products.TotalCount;
        ViewBag.CurrentPage = products.PageNumber;
        ViewBag.HasPreviousPage = products.HasPreviousPage;
        ViewBag.HasNextPage = products.HasNextPage;
        ViewBag.SearchString = searchString;

        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = _productDataStore.CreateProduct(new Product
        {
            Name = product.Name,
            Description = product.Description,
            SalePrice = product.SalePrice,
            SupplyPrice = product.SupplyPrice,
				ExpireDate = product.ExpireDate,
				CategoryId = product.CategoryId 
        });

			if (result is null)
			{
				return BadRequest();
			}

			return RedirectToAction("Details", new { id = result.Id});
		}

    public IActionResult Details(int id)
    {
        var product = _productDataStore.GetProduct(id);

			return View(product);
    }
    public IActionResult Edit(int id)
    {
        var product = _productDataStore.GetProduct(id);
        
        return View(product);
    }
    [HttpPost]
    public IActionResult Edit(int id, string name, string description, decimal salePrice, decimal supplyPrice, DateTime expireDate, int categoryId)
    {
        _productDataStore.UpdateProduct(new Product
        {
            Id = id,
            Name = name,
            Description = description,
            SalePrice = salePrice,
            SupplyPrice = supplyPrice,
            ExpireDate = expireDate,
            CategoryId = categoryId
        });

        return RedirectToAction("Details", new {id});
    }
    public IActionResult Delete(int id)
    {
        _productDataStore.DeleteProduct(id);
        return RedirectToAction(nameof(Index));
    }
}