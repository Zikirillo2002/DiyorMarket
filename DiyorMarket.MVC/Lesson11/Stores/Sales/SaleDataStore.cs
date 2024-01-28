using Lesson11.Models;
using Lesson11.Response;
using Lesson11.Services;
using Newtonsoft.Json;

namespace Lesson11.Stores.Sales
{
    public class SaleDataStore : ISaleDataStore
    {
        private readonly ApiClient _api;

        public SaleDataStore()
        {
            _api = new ApiClient();
        }

        public GetSaleResponse? GetSale()
        {
            var response = _api.Get("sales");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch sales.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<GetSaleResponse>(json);

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
            var response = _api.Put("sales", json);

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
    }
}
