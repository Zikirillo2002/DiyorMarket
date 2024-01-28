namespace DiyorMarket.Domain.DTOs.Supplier
{
    public record SupplierForCreateDto(
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Company);
}
