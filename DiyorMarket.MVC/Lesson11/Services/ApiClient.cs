namespace Lesson11.Services
{
    public class ApiClient
    {
        private const string baseUrl = "https://localhost:7258/api";
        private readonly HttpClient _client = new();

        public ApiClient() 
        {
            _client.BaseAddress = new Uri(baseUrl);
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public HttpResponseMessage Get(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _client.BaseAddress?.AbsolutePath + "/" + url);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjQxMjMiLCJuYW1lIjoiQW52YXIiLCJuYmYiOjE3MDY0MjM1NjIsImV4cCI6MTcwNjQyNzE2MiwiaXNzIjoiYW52YXItYXBpIiwiYXVkIjoiYW52YXItbW9iaWxlIn0.I_obP8tcQZwuiFH4AgSawZ-0qb3zgW-Zv6wMmihio3o");
            var response = _client.Send(request);

            return response;
        }

        public HttpResponseMessage Post(string url, string data)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _client.BaseAddress?.AbsolutePath + "/" + url)
            {
                Content = new StringContent(data)
            };
            var response = _client.Send(request);

            return response;
        }

        public HttpResponseMessage Put(string url, string data)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, _client.BaseAddress?.AbsolutePath + "/" + url)
            {
                Content = new StringContent(data)
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
    }
}
