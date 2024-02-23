using Lesson11.Models;
using Lesson11.Response;

namespace Lesson11.Stores.Sales
{
    public interface ISaleDataStore
    {
        public GetSaleResponse? GetSales(string? searchString, int? customerId, int pageNumber);
        public IEnumerable<Sale> GetCustomersSale(int customersId);
        public Stream GetExportFile();
        public Sale? GetSale(int id);
        public Sale? CreateSale(Sale category);
        public Sale? UpdateSale(Sale category);
        public void DeleteSale(int id);
    }
}
