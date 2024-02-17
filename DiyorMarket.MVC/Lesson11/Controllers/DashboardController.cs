using Lesson11.Stores.Dashboard;
using Lesson11.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lesson11.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardStore _store;

        public DashboardController(IDashboardStore store)
        {
            _store = store;
        }

        public IActionResult Index()
        {
            var dashboard = _store.GetDashboard();

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

            ViewBag.Summary = summary.Total.ToString("0.00");
            ViewBag.SalesCount = summary.SalesCount;
            ViewBag.SuppliesCount = summary.SuppliesCount;
            ViewBag.SalesByCategory = dashboard.SalesByCategories;
            ViewBag.SplineChartData = dashboard.SplineCharts;
            ViewBag.Transactions = dashboard.Transactions;
        }

        private static string ConvertPrice(decimal price)
        {
            var updatedUSD = price * 12400;
            if (updatedUSD / 1_000 > 0)
            {
                return (price / 1_000_000_000).ToString("0.00") + "$";
            }

            return price + " mln";
        }
    }
}
