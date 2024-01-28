namespace DiyorMarket.Domain.DTOs.Sale
{
    public record SaleForUpdateDto(
        int Id,
        DateTime SaleDate,
        int CustomerId);
}
