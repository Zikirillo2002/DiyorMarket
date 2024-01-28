namespace DiyorMarket.Domain.DTOs.Sale
{
    public record SaleForCreateDto(
        DateTime SaleDate,
        int CustomerId);
}
