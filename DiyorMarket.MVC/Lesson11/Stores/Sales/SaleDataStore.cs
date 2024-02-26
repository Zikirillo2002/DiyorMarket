using Lesson11.Models;
using Lesson11.Response;
using Lesson11.Services;
using Newtonsoft.Json;
using System.Text;

namespace Lesson11.Stores.Sales
{
    public class SaleDataStore : ISaleDataStore
    {
        private readonly ApiClient _api;

        public SaleDataStore(ApiClient apiClient)
        {
            _api = apiClient;
        }
        public GetSaleResponse? GetSales(string? searchString, int? customerId, int pageNumber , DateTime?  saleDate)
        {
            StringBuilder query = new("");

            if(saleDate is not null)
            {
                query.Append($"saleDate={saleDate.Value.ToString("MM/dd/yyyy")}&");
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                query.Append($"searchString={searchString}&");
            }

            if (customerId != null)
            {
                query.Append($"customerId={customerId}&");
            }

            if (pageNumber != 0)
            {
                query.Append($"pageNumber={pageNumber}");
            }

            var response = _api.Get("sales?" + query.ToString());

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch sales.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<GetSaleResponse>(json);

            return result;
        }

        public IEnumerable<Sale> GetCustomersSale(int customersId)
        {
            var response = _api.Get($"sales/CustomersSale/{customersId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch sales with id: {customersId}.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<IEnumerable<Sale>>(json);

            return result;
        }

        public Sale? GetSale(int id)
        {
            var response = _api.Get($"sales/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch sales with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<Sale>(json);

            return result;
        }

        public Sale? CreateSale(Sale sale)
        {
            var json = JsonConvert.SerializeObject(sale);
            var response = _api.Post("sales", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating sales.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<Sale>(jsonResponse);
        }

        public Sale? UpdateSale(Sale sale)
        {
            var json = JsonConvert.SerializeObject(sale);
            var response = _api.Put($"sales/{sale.Id}", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating sales.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<Sale>(jsonResponse);
        }

        public void DeleteSale(int id)
        {
            var response = _api.Delete($"sales/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not delete sales with id: {id}.");
            }
        }

        public Stream GetExportFile()
        {
            var response = _api.Get("sales/export");
            var stream = response.Content.ReadAsStream();

            return stream;
        }
    }
}
