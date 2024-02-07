using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson11.Models
{
    public class Supply
    {
        public int Id { get; set; }
        public DateTime SupplyDate { get; set; }
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        public virtual ICollection<SupplyItem>? SupplyItems { get; set; }
    }
}
