namespace DiyorMarket.Domain.DTOs.SaleItem
{
    public record SaleItemDto(
        int Id,
        int Quantity,
        decimal UnitPrice,
        int ProductId,
        int SaleId);
}
