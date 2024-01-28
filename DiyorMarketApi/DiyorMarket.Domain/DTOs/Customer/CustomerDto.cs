using DiyorMarket.Domain.DTOs.Sale;

namespace DiyorMarket.Domain.DTOs.Customer
{
    public record CustomerDto(
        int Id,
        string FirstName,
        string LastName,
        string PhoneNumber,
        ICollection<SaleDto> Sales);
}
