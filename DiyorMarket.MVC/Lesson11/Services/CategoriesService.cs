using Lesson11.Models;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace Lesson11.Services
{
    public class CategoriesService
    {
        private static List<Category> _categories = new List<Category>();
        public CategoriesService()
        {
            LoadDataFromJson();
        }

        public IEnumerable<Category> GetCategories() => _categories;

        public void Create(Category category)
        {
            _categories.Add(category);
            SaveDataToJson();
        }
        public Category? FindById(int id) => _categories.FirstOrDefault(x => x.Id == id);
        public void Update(Category categoryToUpdate)
        {
            var category = FindById(categoryToUpdate.Id);

            if (category != null)
            {
                category.Name = categoryToUpdate.Name;
                SaveDataToJson();
            }
        }

        public void Delete(int id)
        {
            var category = FindById(id);
            _categories.Remove(category);
            SaveDataToJson();
        }

        public void SaveDataToJson()
        {
            string json = JsonConvert.SerializeObject(_categories, Formatting.Indented);
            File.WriteAllText("categories.json", json);
        }
        public void LoadDataFromJson()
        {
            if (File.Exists("categories.json"))
            {
                string json = File.ReadAllText("categories.json");
                _categories = JsonConvert.DeserializeObject<List<Category>>(json);
            }
        }
    }
}
