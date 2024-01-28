using DiyorMarket.Domain.DTOs.SaleItem;

namespace DiyorMarket.Domain.DTOs.Sale
{
    public record SaleDto(
        int Id,
        DateTime SaleDate,
        int CustomerId,
        ICollection<SaleItemDto> SaleItems);
}
