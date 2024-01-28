using DiyorMarket.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiyorMarket.Domain.Entities
{
    public class Sale : EntityBase
    {
        public DateTime SaleDate { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public virtual ICollection<SaleItem>? SaleItems { get; set; }
    }
}
