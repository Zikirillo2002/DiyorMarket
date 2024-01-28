using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Lesson11.Models
{
    [Table(nameof(Category))]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "Category name is required")]
        public string Name { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Product> Products { get; set; }
    }
}
