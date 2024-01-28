using Lesson11.Models;
using Newtonsoft.Json;

namespace Lesson11.Services
{
    public class ProductsService
    {
        private List<Product> _products = new List<Product>();

        public ProductsService()
        {
            LoadDataFromJson();
        }

        public IEnumerable<Product> GetProducts() => _products;

        public void Create(Product product)
        {
            _products.Add(product);
            SaveDataToJson();
        }

        public Product FindById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public void Update(Product productToUpdate)
        {
            var product = FindById(productToUpdate.Id);
            if (product != null)
            {
                product.Name = productToUpdate.Name;
                product.Description = productToUpdate.Description;
                product.SalePrice = productToUpdate.SalePrice;
                product.SupplyPrice = productToUpdate.SupplyPrice;
                SaveDataToJson();
            }
        }

        public void Delete(int id)
        {
            var product = FindById(id);
            if (product != null)
            {
                _products.Remove(product);
                SaveDataToJson();
            }
        }

        private void SaveDataToJson()
        {
            string json = JsonConvert.SerializeObject(_products, Formatting.Indented);
            File.WriteAllText("products.json", json);
        }

        private void LoadDataFromJson()
        {
            if (File.Exists("products.json"))
            {
                string json = File.ReadAllText("products.json");
                _products = JsonConvert.DeserializeObject<List<Product>>(json);
            }
        }
    }
}
