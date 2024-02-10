using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

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
