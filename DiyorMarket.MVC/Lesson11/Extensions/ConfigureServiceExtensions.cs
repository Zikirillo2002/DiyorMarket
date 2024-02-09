using Lesson11.Stores.Categories;
using Lesson11.Stores.Customers;
using Lesson11.Stores.Dashboard;
using Lesson11.Stores.Products;
using Lesson11.Stores.SaleItems;
using Lesson11.Stores.Sales;
using Lesson11.Stores.Suppliers;
using Lesson11.Stores.Supplies;
using Lesson11.Stores.SupplyItems;
using Lesson11.Stores.User;
using Newtonsoft.Json;
using System.Buffers;

namespace Lesson11.Extensions
{
    public static class ConfigureServiceExtensions
    {
        public static IServiceCollection ConfigureDataStores(this IServiceCollection services)
        {
            services.AddScoped<ICategoryDataStore, CategoryDataStore>();
            services.AddScoped<ICustomerDataStore, CustomerDataStore>();
            services.AddScoped<IProductDataStore, ProductDataStore>();
            services.AddScoped<ISaleItemDataStore, SaleItemDataStore>();
            services.AddScoped<ISaleDataStore, SaleDataStore>();
            services.AddScoped<ISupplierDataStore, SupplierDataStore>();
            services.AddScoped<ISupplyDataStore, SupplyDataStore>();
            services.AddScoped<ISupplyItemDataStore, SupplyItemDataStore>();
            services.AddScoped<IDashboardStore, DashboardStore>();
            services.AddScoped<IUserDataStore, UserDataStore>();

            return services;
        }

    }
}
