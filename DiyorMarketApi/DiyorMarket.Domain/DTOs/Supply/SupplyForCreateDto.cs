using DiyorMarket.Domain.DTOs.SupplyItem;

namespace DiyorMarket.Domain.DTOs.Supply
{
    public record SupplyForCreateDto(
        DateTime SupplyDate,
        int SupplierId,
        ICollection<SupplyItemForCreateDto> SupplyItems);
}
