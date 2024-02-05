using Lesson11.Models;
using Lesson11.Response;

namespace Lesson11.Stores.Supplies
{
    public interface ISupplyDataStore
    {
        public GetSupplyResponse? GetSupplies(string? searchString);
        public Supply? GetSupply(int id);
        public Supply? CreateSupply(Supply category);
        public Supply? UpdateSupply(Supply category);
        public void DeleteSupply(int id);
    }
}
