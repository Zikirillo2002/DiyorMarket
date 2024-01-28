namespace DiyorMarket.Domain.Interfaces.Repositories
{
    public interface ICommonRepository
    {
        public IProductRepository Product { get; }
        public ICategoryRepository Category { get; }
        public ICustomerRepository Customer { get; }
        public ISaleRepository Sale { get; }
        public ISaleItemRepository SaleItem { get; }
        public ISupplierRepository Supplier { get; }
        public ISupplyRepository Supply { get; }
        public ISupplyItemRepository SupplyItem { get; }

        public int SaveChanges();
    }
}
