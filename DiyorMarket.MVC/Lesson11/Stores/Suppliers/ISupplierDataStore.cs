using Lesson11.Models;
using Lesson11.Response;

namespace Lesson11.Stores.Suppliers
{
    public interface ISupplierDataStore
    {
        public GetSupplierResponse? GetSuppliers(string? searchString,int pageNumber);
        public Supplier? GetSupplier(int id);
        public Stream GetExportFile();
        public Supplier? CreateSupplier(Supplier category);
        public Supplier? UpdateSupplier(Supplier category);
        public void DeleteSupplier(int id);
    }
}
