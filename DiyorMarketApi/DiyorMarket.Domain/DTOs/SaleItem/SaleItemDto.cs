using DiyorMarket.Domain.DTOs.Product;

namespace DiyorMarket.Domain.DTOs.SaleItem
{
    public record SaleItemDto(
        int Id,
        int Quantity,
        decimal UnitPrice,
        ProductDto Product,
        int SaleId);
}
