using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiyorMarket.Domain.ResourceParameters
{
    public class SupplyItemResourceParameters
    {
        private const int MaxPageSize = 25;

        public int? ProductId { get; set; }
        public int? SupplyId { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? UnitPriceLessThan { get; set; }
        public decimal? UnitPriceGreaterThan { get; set; }
        public string? SearchString { get; set; }
        public string OrderBy { get; set; } = "id";

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 15;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
