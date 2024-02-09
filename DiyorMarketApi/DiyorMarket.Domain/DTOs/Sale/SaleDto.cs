using DiyorMarket.Domain.DTOs.Customer;
using DiyorMarket.Domain.DTOs.SaleItem;

namespace DiyorMarket.Domain.DTOs.Sale
{
    public record SaleDto(
        int Id,
        DateTime SaleDate,
        int CustomerId,
        decimal TotalDue,
        ICollection<SaleItemDto> SaleItems);

    //public class SaleDto
    //{
    //    public int Id;
    //    public DateTime SaleDate;
    //    public CustomerDto Customer;
    //    public decimal TotalDue;
    //    public ICollection<SaleItemDto> SaleItems;
    //}


}
