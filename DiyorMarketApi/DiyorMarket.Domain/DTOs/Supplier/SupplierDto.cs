using DiyorMarket.Domain.DTOs.Supply;

namespace DiyorMarket.Domain.DTOs.Supplier
{
    public record SupplierDto(
        int Id,
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Company,
        ICollection<SupplyDto> Supplies);
}
