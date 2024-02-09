using Lesson11.Models;
using Lesson11.Services;
using Lesson11.ViewModels;
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

        public bool AuthenticateLogin(UserLogin loginViewModel)
        {
            var json = JsonConvert.SerializeObject(loginViewModel);
            var response = _apiClient.Post("auth/login", json);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public bool RegisterLogin(UserLogin registerViewModel)
        {
            var json = JsonConvert.SerializeObject(registerViewModel);
            var response = _apiClient.Post("auth/register", json);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }
    }
}
