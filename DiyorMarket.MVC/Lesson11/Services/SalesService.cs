using Lesson11.Models;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace Lesson11.Services
{
    public class SalesService
    {
        private static List<Sale> _sales = new List<Sale>();
        public SalesService()
        {
            LoadDataFromJson();
        }
        public IEnumerable<Sale> GetCustomers() => _sales;
        public Sale? FindById(int id) => _sales.FirstOrDefault(x => x.Id == id);
        public void Create(Sale sale)
        {
            _sales.Add(sale);
            SaveDataToJson();
        }

        public void Update(Sale saleToUpdate)
        {
            var sale = FindById(saleToUpdate.Id);
            if (sale != null)
            {
                sale.Date = saleToUpdate.Date;

                SaveDataToJson();
            }

        }
        public void Delete(int id)
        {
            var sale = FindById(id);
            _sales.Remove(sale);
            SaveDataToJson();
        }
        public void SaveDataToJson()
        {
            string json = JsonConvert.SerializeObject(_sales, Formatting.Indented);
            File.WriteAllText("sales.json", json);
        }
        public void LoadDataFromJson()
        {
            if (File.Exists("sales.json"))
            {
                string json = File.ReadAllText("sales.json");
                _sales = JsonConvert.DeserializeObject<List<Sale>>(json);
            }
        }
    }
}
