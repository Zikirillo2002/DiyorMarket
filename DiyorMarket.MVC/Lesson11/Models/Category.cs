using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson11.Models
{
    [Table(nameof(Category))]
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int NumberOfProducts { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
