using Lesson11.ViewModels;

namespace Lesson11.Stores.Dashboard
{
    public interface IDashboardStore
    {
        public DashboardViewModel? GetDashboard();
    }
}
