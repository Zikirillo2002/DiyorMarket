using DiyorMarket.Domain.Common;
using System.Text.Json.Serialization;

namespace DiyorMarket.Domain.Entities
{
    public class Supply : EntityBase
    {
        public int Id { get; set; }
        public DateTime SupplyDate { get; set; }

        public int SupplierId { get; set; }
        [JsonIgnore]
        public Supplier Supplier { get; set; }

        public virtual ICollection<SupplyItem> SupplyItems { get; set; }
    }
}
