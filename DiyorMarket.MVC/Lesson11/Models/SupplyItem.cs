using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson11.Models
{
    [Table(nameof(SupplyItem))]
    public class SupplyItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public int SupplyId { get; set; }
        [ForeignKey(nameof(SupplyId))]
        public Supply Supply { get; set; }
    }
}
