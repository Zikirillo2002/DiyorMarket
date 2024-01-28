using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson11.Models
{
    [Table(nameof(Product))]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal SalePrice { get; set; }
        [Required]
        public decimal SupplyPrice { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        public virtual ICollection<SaleItem> SaleItems { get; set; }
        public virtual ICollection<SupplyItem> SupplyItems { get; set; }
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }

        public Product()
        {
            SaleItems = new List<SaleItem>();
            SupplyItems = new List<SupplyItem>();
            InventoryItems = new List<InventoryItem>();
        }
    }
}
