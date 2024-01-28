using DiyorMarket.Domain.DTOs.Category;
using DiyorMarket.Domain.DTOs.Supply;
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
    public interface ISupplyService
    {
        GetSupplyResponse GetSupplies(SupplyResourceParameters supplyResourceParameters);
        SupplyDto? GetSupplyById(int id);
        SupplyDto CreateSupply(SupplyForCreateDto supplyToCreate);
        void UpdateSupply(SupplyForUpdateDto supplyToUpdate);
        void DeleteSupply(int id);
    }
}
