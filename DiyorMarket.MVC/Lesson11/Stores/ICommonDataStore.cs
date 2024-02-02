using Lesson11.Stores.Categories;
using Lesson11.Stores.Customers;
using Lesson11.Stores.Products;
using Lesson11.Stores.SaleItems;
using Lesson11.Stores.Sales;
using Lesson11.Stores.Suppliers;
using Lesson11.Stores.Supplies;
using Lesson11.Stores.SupplyItems;

namespace Lesson11.Stores
{
    public interface ICommonDataStore
    {
        public ICategoryDataStore Categories { get; }
        public IProductDataStore Products { get; }
        public ICustomerDataStore Customers { get; }
        public ISaleDataStore Sales { get; }
        public ISaleItemDataStore SaleItems { get; }
        public ISupplyDataStore Supplies { get; }
        public ISupplyItemDataStore SupplyItems { get; }
        public ISupplierDataStore Suppliers { get; }

        public int SaveChanges();
    }
}
