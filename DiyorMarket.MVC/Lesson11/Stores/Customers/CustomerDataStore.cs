using Lesson11.Models;
using Lesson11.Response;
using Lesson11.Services;
using Newtonsoft.Json;
using System.Text;

namespace Lesson11.Stores.Customers
{
    public class CustomerDataStore : ICustomerDataStore
    {
        private readonly ApiClient _api;
        public CustomerDataStore()
        {
            _api = new ApiClient();
        }

        public GetCustomerResponse? GetCustomers(string? searchString, int pageNumber)
        {
            StringBuilder query = new("");

            if (!string.IsNullOrEmpty(searchString))
            {
                query.Append($"searchString={searchString}&");
            }

            if (pageNumber != 0)
            {
                query.Append($"pageNumber={pageNumber}&");
            }

            var response = _api.Get("customers?" + query.ToString());

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch customers.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<GetCustomerResponse>(json);

            return result;
        }

        public Customer? GetCustomer(int id)
        {
            var response = _api.Get($"customers/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not fetch customers with id: {id}.");
            }

            var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var result = JsonConvert.DeserializeObject<Customer>(json);

            return result;
        }

        public Customer? CreateCustomer(Customer customer)
        {
            var json = JsonConvert.SerializeObject(customer);
            var response = _api.Post("customers", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating customer.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<Customer>(jsonResponse);
        }

        public Customer? UpdateCustomer(Customer category)
        {
            var json = JsonConvert.SerializeObject(category);
            var response = _api.Put($"customers/{category.Id}", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error updating customers.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<Customer>(jsonResponse);
        }

        public void DeleteCustomer(int id)
        {
            var response = _api.Delete($"customers/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Could not delete customers with id: {id}.");
            }
        }

        public Stream GetExportFile()
        {
            var response = _api.Get("customers/export");
            var stream = response.Content.ReadAsStream();

            return stream;
        }
    }
}
