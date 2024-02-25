using Lesson11.Constants;
using Lesson11.Exceptions;
using NuGet.Common;

namespace Lesson11.Services
{
    public class ApiClient
    {
        private const string baseUrl = "https://localhost:7258/api";
        private readonly HttpClient _client = new();
        private readonly IHttpContextAccessor _contextAccessor;

        public ApiClient(IHttpContextAccessor contextAccessor)
        {
            _client.BaseAddress = new Uri(baseUrl);
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        public HttpResponseMessage Get(string url)
        {
            string token = string.Empty;
            var request = new HttpRequestMessage(HttpMethod.Get, _client.BaseAddress?.AbsolutePath + "/" + url);
            _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(Configurations.JwtToken, out token);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = _client.Send(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error fetching url: {url}");
            }

            return response;
        }

        public HttpResponseMessage Post(string url, string data)
        {
            string token = string.Empty;
            var request = new HttpRequestMessage(HttpMethod.Post, _client.BaseAddress?.AbsolutePath + "/" + url)
            {
                Content = new StringContent(data, new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"))
            };
            _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(Configurations.JwtToken, out token);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = _client.Send(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException(response.StatusCode, $"Error fetching url: {url}");
            }

            return response;
        }

        public HttpResponseMessage Put(string url, string data)
        {
            var token = string.Empty;
            var request = new HttpRequestMessage(HttpMethod.Put, _client.BaseAddress?.AbsolutePath + "/" + url)
            {
                Content = new StringContent(data, new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"))
            };
            _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(Configurations.JwtToken, out token);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = _client.Send(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException(response.StatusCode, $"Error fetching url: {url}");
            }

            return response;
        }

        public HttpResponseMessage Delete(string url)
        {
            string token = string.Empty;
            var request = new HttpRequestMessage(HttpMethod.Delete, _client.BaseAddress?.AbsolutePath + "/" + url);
            _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(Configurations.JwtToken, out token);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = _client.Send(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException(response.StatusCode, $"Error fetching url: {url}");
            }

            return response;
        }
    }
}
