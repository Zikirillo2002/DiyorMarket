using DiyorMarket.Domain.DTOs.Sale;
using DiyorMarket.Domain.Pagniation;
using DiyorMarket.Domain.ResourceParameters;

namespace DiyorMarket.Domain.Interfaces.Services
{
    public interface ISaleService
    {
        PaginatedList<SaleDto> GetSales(SaleResourceParameters saleResourceParameters);
        SaleDto? GetSaleById(int id);
        SaleDto CreateSale(SaleForCreateDto saleToCreate);
        void UpdateSale(SaleForUpdateDto saleToUpdate);
        void DeleteSale(int id);
    }
}
