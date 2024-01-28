using Lesson11.Models;
using Lesson11.Response;

namespace Lesson11.Stores.SupplyItems
{
    public interface ISupplyItemDataStore
    {
        public GetSupplyItemResponse? GetSupplyItem();
        public SupplyItem? GetSupplyItem(int id);
        public SupplyItem? CreateSupplyItem(SupplyItem category);
        public SupplyItem? UpdateSupplyItem(SupplyItem category);
        public void DeleteSupplyItem(int id);
    }
}
