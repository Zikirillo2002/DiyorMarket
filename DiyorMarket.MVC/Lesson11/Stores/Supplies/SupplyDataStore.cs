using Lesson11.Models;
using Lesson11.Response;
using Lesson11.Services;
using Newtonsoft.Json;
using Syncfusion.EJ2.Diagrams;

namespace Lesson11.Stores.Supplies
{
    public class SupplyDataStore : ISupplyDataStore
    {
        private readonly ApiClient _api;

        public SupplyDataStore()
        {
            _api = new ApiClient(); 
        }

        public GetSupplyResponse? GetSupply()
        {
            var response = _api.Get("supplies");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch supplies.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<GetSupplyResponse>(json);

            return result;
        }

        public Supply? GetSupply(int id)
        {
            var response = _api.Get($"supplies/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch supplies with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<Supply>(json);

            return result;
        }

        public Supply? CreateSupply(Supply category)
        {
            var json = JsonConvert.SerializeObject(category);
            var response = _api.Post("supplies", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating supplies.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<Supply>(jsonResponse);
        }
        public Supply? UpdateSupply(Supply category)
        {
            var json = JsonConvert.SerializeObject(category);
            var response = _api.Put("supplies", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating supplies.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<Supply>(jsonResponse);
        }

        public void DeleteSupply(int id)
        {
            var response = _api.Delete($"supplies/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not delete supplies with id: {id}.");
            }
        }
    }
}
