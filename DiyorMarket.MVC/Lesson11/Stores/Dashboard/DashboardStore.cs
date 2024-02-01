using Lesson11.Services;
using Lesson11.ViewModels;
using Newtonsoft.Json;

namespace Lesson11.Stores.Dashboard
{
    public class DashboardStore : IDashboardStore
    {
        private readonly ApiClient _client;

        public DashboardStore()
        {
            _client = new ApiClient();
        }

        public DashboardViewModel? GetDashboard()
        {
            var response = _client.Get("Dashboard?searchString={searchString}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error occured while fetching dashboard data.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<DashboardViewModel>(json);

            return result;
        }
    }
}
