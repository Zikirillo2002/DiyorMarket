using Lesson11.Stores;
using Lesson11.Stores.Dashboard;
using Lesson11.Stores.Information;
using Lesson11.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lesson11.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardStore _store;

        public DashboardController(IDashboardStore store)
        {
            _store = store;
        }

        public IActionResult Index(string searchString)
        {
            var dashboard = _store.GetDashboard(sea);

            if (dashboard is null)
            {
                return BadRequest();
            }

            SetViewBagProperties(dashboard);

            return View();
        }

        private void SetViewBagProperties(DashboardViewModel dashboard)
        {
            var summary = dashboard.Summary;

            ViewBag.Summary = ConvertPrice(summary.Total);
            ViewBag.SalesCount = summary.SalesCount;
            ViewBag.SuppliesCount = summary.SuppliesCount;
            ViewBag.SalesByCategory = dashboard.SalesByCategories;
            ViewBag.SplineChartData = dashboard.SplineCharts;
            ViewBag.Transactions = dashboard.Transactions;
        }

        private static string ConvertPrice(decimal price)
        {
            if (price / 1_000_000_000 > 0)
            {
                return (price / 1_000_000_000).ToString("0.00") + " mlrd";
            }

            return price + " mln";
        }
    }
}
