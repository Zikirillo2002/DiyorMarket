using DiyorMarket.Infrastructure.Persistence;

namespace Lesson11.Stores.Information
{
    public interface IInformationsDataStore
    {
        public DiyorMarketDbContext? GetDbContext();
    }
}
