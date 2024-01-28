using DiyorMarket.Domain.DTOs.Supplier;
using DiyorMarket.Domain.Pagniation;
using DiyorMarket.Domain.ResourceParameters;
using DiyorMarket.Domain.Responses;

namespace DiyorMarket.Domain.Interfaces.Services
{
    public interface ISupplierService
    {
        GetSupplierResponse GetSuppliers(SupplierResourceParameters supplierResourceParameters);
        SupplierDto? GetSupplierById(int id);
        SupplierDto CreateSupplier(SupplierForCreateDto supplierToCreate);
        void UpdateSupplier(SupplierForUpdateDto supplierToUpdate);
        void DeleteSupplier(int id);
    }
}
