using DiyorMarket.Domain.DTOs.SaleItem;
using DiyorMarket.Domain.DTOs.SupplyItem;

namespace DiyorMarket.Domain.DTOs.Product
{
    public record ProductDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal SalePrice { get; init; }
        public decimal SupplyPrice { get; init; }
        public DateTime ExpireDate { get; init; }
        public int CategoryId { get; init; }
        public ICollection<SaleItemDto> SaleItems { get; init; }
        public ICollection<SupplyItemDto> SupplyItems { get; init; }

        //public ProductDto(int id, string name, string description, decimal salePrice, decimal supplyPrice, DateTime expireDate, int categoryId, ICollection<SaleItemDto>? saleItems, ICollection<SupplyItemDto>? supplyItems)
        //{
        //}
    }
}
