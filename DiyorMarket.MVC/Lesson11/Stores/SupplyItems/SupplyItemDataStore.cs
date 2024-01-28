using Lesson11.Models;
using Lesson11.Response;
using Lesson11.Services;
using Newtonsoft.Json;

namespace Lesson11.Stores.SupplyItems
{
    public class SupplyItemDataStore : ISupplyItemDataStore
    {
        private readonly ApiClient _api;

        public SupplyItemDataStore()
        {
            _api = new ApiClient();
        }

        public GetSupplyItemResponse? GetSupplyItems()
        {
            var response = _api.Get("supplyItems");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch supplyItems.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<GetSupplyItemResponse>(json);

            return result;
        }

        public SupplyItem? GetSupplyItem(int id)
        {
            var response = _api.Get($"supplyItems/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch supplyItems with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<SupplyItem>(json);

            return result;
        }

        public SupplyItem? CreateSupplyItem(SupplyItem category)
        {
            var json = JsonConvert.SerializeObject(category);
            var response = _api.Post("supplyItems", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating supplyItems.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<SupplyItem>(jsonResponse);
        }
        public SupplyItem? UpdateSupplyItem(SupplyItem category)
        {
            var json = JsonConvert.SerializeObject(category);
            var response = _api.Put("supplyItems", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating supplyItems.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<SupplyItem>(jsonResponse);
        }

        public void DeleteSupplyItem(int id)
        {
            var response = _api.Delete($"supplyItems/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not delete supplyItems with id: {id}.");
            }
        }
    }
}
