using DiyorMarket.Domain.DTOs.SupplyItem;
using DiyorMarket.Domain.Pagniation;
using DiyorMarket.Domain.ResourceParameters;

namespace DiyorMarket.Domain.Interfaces.Services
{
    public interface ISupplyItemService
    {
        PaginatedList<SupplyItemDto> GetSupplyItems(SupplyItemResourceParameters supplyItemResourceParameters);
        SupplyItemDto? GetSupplyItemById(int id);
        SupplyItemDto CreateSupplyItem(SupplyItemForCreateDto supplyItemToCreate);
        void UpdateSupplyItem(SupplyItemForUpdateDto supplyItemToUpdate);
        void DeleteSupplyItem(int id);
    }
}
