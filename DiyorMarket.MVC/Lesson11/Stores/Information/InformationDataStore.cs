using DiyorMarket.Infrastructure.Persistence;
using Lesson11.Services;
using Newtonsoft.Json;

namespace Lesson11.Stores.Information
{
    public class InformationDataStore : IInformationsDataStore
    {
        private readonly ApiClient _api;

        public InformationDataStore()
        {
            _api = new ApiClient();
        }

        public DiyorMarketDbContext? GetDbContext()
        {
            var response = _api.Get("informations");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch dbcontext.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<DiyorMarketDbContext>(json);

            return result;
        }
    }
}
