using DiyorMarket.Domain.DTOs.SaleItem;

namespace DiyorMarket.Domain.DTOs.Sale
{
    public record SaleForCreateDto(
        DateTime SaleDate,
        int CustomerId,
        ICollection<SaleItemForCreateDto> SaleItems);
}
