using DiyorMarket.Domain.DTOs.Category;
using DiyorMarket.Domain.DTOs.SaleItem;
using DiyorMarket.Domain.Pagniation;
using DiyorMarket.Domain.ResourceParameters;
using DiyorMarket.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiyorMarket.Domain.Interfaces.Services
{
    public interface ISaleItemService
    {
        GetSaleItemResponse GetSaleItems(SaleItemResourceParameters saleItemResourceParameters);
        SaleItemDto? GetSaleItemById(int id);
        SaleItemDto CreateSaleItem(SaleItemForCreateDto saleItemToCreate);
        void UpdateSaleItem(SaleItemForUpdateDto saleItemToUpdate);
        void DeleteSaleItem(int id);
    }
}
