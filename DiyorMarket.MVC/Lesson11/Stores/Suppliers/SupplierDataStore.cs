using Lesson11.Models;
using Lesson11.Response;
using Lesson11.Services;
using Newtonsoft.Json;
using System.Text;

namespace Lesson11.Stores.Suppliers
{
    public class SupplierDataStore : ISupplierDataStore
    {
        private readonly ApiClient _api;

        public SupplierDataStore()
        {
            _api = new ApiClient();
        }
        public GetSupplierResponse? GetSuppliers(string? searchString, int pageNumber)
        {
            StringBuilder query = new("");

            if (!string.IsNullOrEmpty(searchString))
            {
                query.Append($"searchString={searchString}&");
            }
            if (pageNumber != 0) 
            {
                query.Append($"pageNumber={pageNumber}");
            }

            var response = _api.Get("suppliers?" + query.ToString());

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch suppliers.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<GetSupplierResponse>(json);

            return result;
        }

        public Supplier? GetSupplier(int id)
        {
            var response = _api.Get($"suppliers/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch suppliers with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<Supplier>(json);

            return result;
        }
        public Supplier? CreateSupplier(Supplier category)
        {
            var json = JsonConvert.SerializeObject(category);
            var response = _api.Post("suppliers", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating suppliers.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<Supplier>(jsonResponse);
        }
        public Supplier? UpdateSupplier(Supplier category)
        {
            var json = JsonConvert.SerializeObject(category);
            var response = _api.Put($"suppliers/{category.Id}", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating suppliers.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<Supplier>(jsonResponse);
        }

        public void DeleteSupplier(int id)
        {
            var response = _api.Delete($"suppliers/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not delete suppliers with id: {id}.");
            }
        }

        public Stream GetExportFile()
        {
            var response = _api.Get("suppliers/export");
            var stream = response.Content.ReadAsStream();

            return stream;
        }
    }
}
