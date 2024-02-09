namespace DiyorMarket.Domain.DTOs.Customer
{
    public record CustomerForUpdateDto(
        int Id,
        string FullName,
        string PhoneNumber);
}
