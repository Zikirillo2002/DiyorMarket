using DiyorMarket.Domain.DTOs.Sale;

namespace DiyorMarket.Domain.DTOs.Customer
{
    public record CustomerDto(
        int Id,
        string FullName,
        string PhoneNumber,
        ICollection<SaleDto> Sales);
}
