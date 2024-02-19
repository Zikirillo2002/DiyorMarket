using Lesson11.Models;
using Lesson11.Response;

namespace Lesson11.Stores.Supplies
{
    public interface ISupplyDataStore
    {
        public GetSupplyResponse? GetSupplies(string? searchString, int? supplierId, int pageNumber);
        public Supply? GetSupply(int id);
        public Stream GetExportFile();
        public Supply? CreateSupply(Supply category);
        public Supply? UpdateSupply(Supply category);
        public void DeleteSupply(int id);
    }
}
