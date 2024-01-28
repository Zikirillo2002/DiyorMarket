using DiyorMarket.Domain.DTOs.SaleItem;
using DiyorMarket.Domain.DTOs.SupplyItem;
using DiyorMarket.Domain.Entities;

namespace DiyorMarket.Domain.DTOs.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpireDate { get; set; }
        public int CategoryId { get; set; }
        public ICollection<SaleItemDto> SaleItems { get; set; } = new List<SaleItemDto>();
        public ICollection<SupplyItemDto> SupplyItems { get; set; } = new List<SupplyItemDto>();
    }
        
}
