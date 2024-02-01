using DiyorMarket.Domain.DTOs.SaleItem;
using DiyorMarket.Domain.DTOs.SupplyItem;

namespace DiyorMarket.Domain.DTOs.Product
{
    public record ProductDto(
        int Id,
        string Name,
        string Description,
        decimal SalePrice,
        decimal SupplyPrice,
        DateTime ExpireDate,
        int CategoryId,
        ICollection<SaleItemDto> SaleItems,
        ICollection<SupplyItemDto> SupplyItems
        );   
}
