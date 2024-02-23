using System.ComponentModel;

namespace DiyorMarket.Domain.ResourceParameters
{
    public class SaleResourceParameters
    {
        private const int MaxPageSize = 25;

        public int? CustomerId { get; set; }
        public string? SearchString { get; set; }
        public string OrderBy { get; set; } = "int";
        public DateTime? SaleDate { get; set; }
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 15;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
