using NuGet.Common;

namespace Lesson11.Services
{
    public class ApiClient
    {
        private const string baseUrl = "https://localhost:7258/api";
        private readonly HttpClient _client = new();
        private readonly string _apiToken;

        public ApiClient()
        {
            _client.BaseAddress = new Uri(baseUrl);
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _apiToken = ReadFromJsonFile();
        }

        public ApiClient(string apiToken)
            : this()
        {
            _apiToken = apiToken;
        }

        public HttpResponseMessage Get(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _client.BaseAddress?.AbsolutePath + "/" + url);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                _apiToken);
            var response = _client.Send(request);

            return response;
        }

        public HttpResponseMessage Post(string url, string data)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _client.BaseAddress?.AbsolutePath + "/" + url)
            {
                Content = new StringContent(data, new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"))
            };
            var response = _client.Send(request);

            return response;
        }

        public HttpResponseMessage Put(string url, string data)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, _client.BaseAddress?.AbsolutePath + "/" + url)
            {
                Content = new StringContent(data, new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"))
            };
            var response = _client.Send(request);

            return response;
        }

        public HttpResponseMessage Delete(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, _client.BaseAddress?.AbsolutePath + "/" + url);
            var response = _client.Send(request);

            return response;
        }

        private string ReadFromJsonFile()
        {
            string path = Directory.GetCurrentDirectory() + "\\JsonFile\\UserToken.json";
            string token = "";

            if (!File.Exists(path))
            {
                return token;
            }

            using (StreamReader sr = new StreamReader(path))
            {
                token = sr.ReadToEnd().ToString();
            }

            return token.Trim();
        }
    }
}
