using DiyorMarket.Domain.DTOs.Sale;
using DiyorMarket.Domain.Pagniation;
using DiyorMarket.Domain.ResourceParameters;
using DiyorMarket.Domain.Responses;

namespace DiyorMarket.Domain.Interfaces.Services
{
    public interface ISaleService
    {
        GetSaleResponse GetSales(SaleResourceParameters saleResourceParameters);
        SaleDto? GetSaleById(int id);
        SaleDto CreateSale(SaleForCreateDto saleToCreate);
        void UpdateSale(SaleForUpdateDto saleToUpdate);
        void DeleteSale(int id);
    }
}
