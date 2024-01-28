using Lesson11.Models;
using Lesson11.Response;
using Lesson11.Services;
using Newtonsoft.Json;

namespace Lesson11.Stores.SaleItems
{
    public class SaleItemDataStore : ISaleItemDataStore
    {
        private readonly ApiClient _api;

        public SaleItemDataStore()
        {
            _api = new ApiClient();
        }

        public GetSaleItemResponse? GetSaleItem()
        {
            var response = _api.Get("saleItems");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch saleItems.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<GetSaleItemResponse>(json);

            return result;
        }

        public SaleItem? GetSaleItem(int id)
        {
            var response = _api.Get($"saleItems/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch saleItems with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<SaleItem>(json);

            return result;
        }

        public SaleItem? CreateSaleItem(SaleItem saleItem)
        {
            var json = JsonConvert.SerializeObject(saleItem);
            var response = _api.Post("saleItems", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating saleItems.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<SaleItem>(jsonResponse);
        }

        public SaleItem? UpdateSaleItem(SaleItem saleItem)
        {
            var json = JsonConvert.SerializeObject(saleItem);
            var response = _api.Put("saleItems", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating saleItems.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<SaleItem>(jsonResponse);
        }

        public void DeleteSaleItem(int id)
        {
            var response = _api.Delete($"saleItems/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not delete saleItems with id: {id}.");
            }
        }
    }
}
