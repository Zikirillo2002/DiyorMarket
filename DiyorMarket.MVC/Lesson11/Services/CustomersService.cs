using Lesson11.Models;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace Lesson11.Services
{
    public class CustomersService
    {
        private static List<Customer> _customers = new List<Customer>();
        public CustomersService()
        {
            LoadDataFromJson();
        }
        public IEnumerable<Customer> GetCustomers() => _customers;
        public Customer? FindById(int id) => _customers.FirstOrDefault(x => x.Id == id);
        public void Create(Customer customer)
        {
            _customers.Add(customer);
            SaveDataToJson();
        }

        public void Update(Customer customerToUpdate)
        {
            var customer = FindById(customerToUpdate.Id);
            if (customer != null)
            {
                customer.FirstName = customerToUpdate.FirstName;
                customer.LastName = customerToUpdate.LastName;
                customer.PhoneNumber = customerToUpdate.PhoneNumber;
                SaveDataToJson();
            }

        }
        public void Delete(int id)
        {
            var customer = FindById(id);
            _customers.Remove(customer);
            SaveDataToJson();
        }
        public void SaveDataToJson()
        {
            string json = JsonConvert.SerializeObject(_customers, Formatting.Indented);
            File.WriteAllText("customers.json", json);
        }
        public void LoadDataFromJson()
        {
            if (File.Exists("customers.json"))
            {
                string json = File.ReadAllText("customers.json");
                _customers = JsonConvert.DeserializeObject<List<Customer>>(json);
            }
        }
    }
}
