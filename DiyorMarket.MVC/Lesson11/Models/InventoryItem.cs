using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson11.Models
{
    [Table(nameof(InventoryItem))]
    public class InventoryItem
    {
        [Key]
        public int Id { get; set; }
        public int QuantityInStock { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public int InventoryId { get; set; }
        [ForeignKey(nameof(InventoryId))]
        public Inventory Inventory { get; set; }
    }
}
