namespace Lesson11.ViewModels
{
    public class DashboardViewModel
    {
        public SummaryViewModel Summary { get; set; }
        public IEnumerable<SalesByCategoryViewModel> SalesByCategories { get; set; }
        public IEnumerable<SpliteChartData> SplineCharts { get; set; }
        public IEnumerable<TransactionView> Transactions { get; set; }
    }

    public class SummaryViewModel
    {
        public decimal Total { get; set; }
        public int SalesCount { get; set; }
        public int SuppliesCount { get; set; }
    }

    public class SalesByCategoryViewModel
    {
        public string Category { get; set; }
        public int SalesCount { get; set; }
    }

    public class SpliteChartData
    {
        public string Month { get; set; }
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
    }

    public class TransactionView
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
