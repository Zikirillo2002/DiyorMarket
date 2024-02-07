using DiyorMarket.Domain.DTOs.Supplier;
using DiyorMarket.Domain.DTOs.SupplyItem;

namespace DiyorMarket.Domain.DTOs.Supply
{
    public record SupplyDto(
        int Id,
        DateTime SupplyDate,
        SupplierDto Supplier,
        ICollection<SupplyItemDto> SupplyItems);
}
