using DiyorMarket.Domain.DTOs.Customer;
using DiyorMarket.Domain.DTOs.SaleItem;

namespace DiyorMarket.Domain.DTOs.Sale
{
    public record SaleDto(
        int Id,
        DateTime SaleDate,
        CustomerDto Customer,
        decimal TotalDue,
        ICollection<SaleItemDto> SaleItems);
}
