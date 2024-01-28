using Lesson11.Models;
using Lesson11.Response;

namespace Lesson11.Stores.SaleItems
{
    public interface ISaleItemDataStore
    {
        public GetSaleItemResponse? GetSaleItems();
        public SaleItem? GetSaleItem(int id);
        public SaleItem? CreateSaleItem(SaleItem category);
        public SaleItem? UpdateSaleItem(SaleItem category);
        public void DeleteSaleItem(int id);
    }
}
