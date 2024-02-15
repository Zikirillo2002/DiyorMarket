using Lesson11.Models;
using Lesson11.Services;
using Newtonsoft.Json;

namespace Lesson11.Stores.User
{
    public class UserDataStore : IUserDataStore
    {
        private readonly ApiClient _apiClient;
        public UserDataStore()
        {
            _apiClient = new ApiClient();
        }

        public (bool,string) AuthenticateLogin(UserLogin loginViewModel)
        {
            var json = JsonConvert.SerializeObject(loginViewModel);
            var response = _apiClient.Post("auth/login", json);

            if (!response.IsSuccessStatusCode)
            {
                return (false, null);
            }

            var tokenJson = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var token = JsonConvert.DeserializeObject<string>(tokenJson);

            return (true, tokenJson);
        }

        public (bool, string) RegisterLogin(UserLogin registerViewModel)
        {
            var json = JsonConvert.SerializeObject(registerViewModel);
            var response = _apiClient.Post("auth/register", json);

            if (!response.IsSuccessStatusCode)
            {
                return (false, null);
            }

            var tokenJson = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var token = JsonConvert.DeserializeObject<string>(tokenJson);

            return (true, tokenJson);
        }

    }
}
