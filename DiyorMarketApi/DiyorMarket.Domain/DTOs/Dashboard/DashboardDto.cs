namespace DiyorMarket.Domain.DTOs.Dashboard;

public record DashboardDto(Summary Summary, 
    IEnumerable<SalesByCategoryDto> SalesByCategories,
    IEnumerable<SpliteChartData> SplineCharts,
    IEnumerable<TransactionDto> Transactions);
public record Summary(decimal Total, int SalesCount, int SuppliesCount);
public record SalesByCategoryDto (string Category, int SalesCount);
public class SpliteChartData
{
    public string Month { get; set; }
    public decimal Income { get; set; }
    public decimal Expense { get; set; }
}
public class TransactionDto
{
    public int Id { get; set; }
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}