using ExcelDataReader;
using Lesson11.Models;
using Lesson11.Stores.Categories;
using Lesson11.Stores.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    public IActionResult Index(string? searchString, int? categoryId, int pageNumber, int? prevCategoryId)
    {
        if (categoryId == null && prevCategoryId != null)
        {
            categoryId = prevCategoryId;
        }

        var filteredProducts = _productDataStore.GetProducts(searchString, categoryId, pageNumber);

        var categories = GetAllCategories();

        categories.Insert(0, new Category
        {
            Id = 0,
            Name = "All"
        });

        var selectCategory = categories[0];

        if (categoryId.HasValue && categoryId != 0)
        {
            selectCategory = categories.FirstOrDefault(x => x.Id == categoryId);
        }

        ViewBag.Products = filteredProducts.Data;
        ViewBag.SelectedCategory = selectCategory;

        ViewBag.Categories = categories;
        ViewBag.PageSize = filteredProducts.PageSize;
        ViewBag.PageCount = filteredProducts.TotalPages;
        ViewBag.TotalCount = filteredProducts.TotalCount;
        ViewBag.CurrentPage = filteredProducts.PageNumber;
        ViewBag.HasPreviousPage = filteredProducts.HasPreviousPage;
        ViewBag.HasNextPage = filteredProducts.HasNextPage;
        ViewBag.CurrentCategoryId = categoryId;
        ViewBag.SearchString = searchString;



        return View();
    }

    public IActionResult Create()
    {
        ViewData["Category"] = new SelectList(GetAllCategories(), "Id", "Name");
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

        ViewData["Category"] = new SelectList(GetAllCategories(), "Id", "Name", result.CategoryId);

        return RedirectToAction("Details", new { id = result.Id});
    }

    public IActionResult Details(int id)
    {
        var product = _productDataStore.GetProduct(id);

			return View(product);
    }

    public IActionResult Upload()
    {
        ViewBag.FileUploaded = false;
        return View();
    }

    [HttpPost]
    public IActionResult Upload(IFormFile file)
    {
        if (file is null)
        {
            ViewBag.FileUploaded = false;
            return View();
        }

        var customers = DeserializeFile(file);

        ViewBag.Products = customers;
        ViewBag.FileUploaded = true;

        return View();
    }

    public IActionResult Download()
    {
        var result = _categoryDataStore.GetExportFile();

        return File(result, "application/xls", "Products.xls");
    }

    public IActionResult Edit(int id)
    {
        var product = _productDataStore.GetProduct(id);
        ViewData["Category"] = new SelectList(GetAllCategories(), "Id", "Name", product.CategoryId);

        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(int id, string name, string description,
        decimal salePrice, decimal supplyPrice, DateTime expireDate, int categoryId)
    {
        var updaetProduct = _productDataStore.UpdateProduct(new Product
        {
            Id = id,
            Name = name,
            Description = description,
            SalePrice = salePrice,
            SupplyPrice = supplyPrice,
            ExpireDate = expireDate,
            CategoryId = categoryId
        });

        ViewData["Category"] = new SelectList(GetAllCategories(), "Id", "Name", updaetProduct.CategoryId);

        return RedirectToAction("Details", new { id });
    }

    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = _productDataStore.GetProduct((int)id);

        if (product == null)
        {
            return NotFound(product);
        }
        return View();
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult Delete(int id)
    {
        _productDataStore.DeleteProduct(id);

        return RedirectToAction(nameof(Index));
    }

    private List<Category> GetAllCategories()
    {
        int number = 1;
        var categoryResponse = _categoryDataStore.GetCategories(null, number);
        var categories = categoryResponse.Data.ToList();


        for (int i = 1; i <= categoryResponse.TotalPages; i++)
        {
            categoryResponse = _categoryDataStore.GetCategories(null, ++number);
            categories.AddRange(categoryResponse.Data.ToList());
        }

        return categories;
    }

    private static List<Product> DeserializeFile(IFormFile file)
    {
        List<Product> products = new();

        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        using var stream = new MemoryStream();
        file.CopyTo(stream);
        stream.Position = 0;
        using var reader = ExcelReaderFactory.CreateReader(stream);

        while (reader.Read())
        {
            products.Add(new Product
            {
                Name = reader.GetValue(0)?.ToString(),
                Description = reader.GetValue(1)?.ToString(),
                SalePrice = Convert.ToDecimal(reader.GetValue(2)),
                SupplyPrice = Convert.ToDecimal(reader.GetValue(3)),
                ExpireDate = Convert.ToDateTime(reader.GetValue(4)),
                CategoryId = Convert.ToInt32(reader.GetValue(5))
            }); ;
        }

        return products;
    }
}
        
    
