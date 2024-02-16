using DiyorMarket.Domain.DTOs.Dashboard;
using DiyorMarket.Domain.Interfaces.Services;
using DiyorMarket.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DiyorMarket.Services;

public class DashboardService : IDashboardService
{
    private readonly DiyorMarketDbContext _context;

    public DashboardService(DiyorMarketDbContext context)
    {
        _context = context;
    }

    public DashboardDto GetDashboard()
    {
        var summary = GetSummary();
        var salesByCategory = GetDoughChartData();
        var splineChartData = GetSpliteChartData();
        var transactions = GetTransactions();
        
        return new DashboardDto(summary, salesByCategory, splineChartData, transactions);
    }

    private IEnumerable<SalesByCategoryDto> GetDoughChartData()
    {
        var salesByCategory = from category in _context.Categories
                              join product in _context.Products on category.Id equals product.CategoryId
                              join saleItem in _context.SaleItems on product.Id equals saleItem.ProductId
                              group saleItem by category.Name into groupedCategories
                              select new SalesByCategoryDto(groupedCategories.Key, groupedCategories.Count());


        return salesByCategory;
    }

    private Summary GetSummary()
    {
        //2 month sales
        var salesItems = _context.SaleItems
            .Where(x => x.Sale.SaleDate.Month >= DateTime.Now.AddMonths(-2).Month)
            .AsNoTracking();

        var salesTotal = salesItems.Sum(si => si.Quantity * si.UnitPrice);

        var total = salesTotal * (decimal)0.2;

        var salesCount = _context.Sales.Count();
        var suppliesCount = _context.Supplies.Count();

        return new Summary(total, salesCount, suppliesCount);
    }

    private IEnumerable<SpliteChartData> GetSpliteChartData()
    {
        var startDate = DateTime.Now.AddYears(-1);

        var incomeTotal = _context.Sales
            .Include(x => x.SaleItems)
            .ToList()
            .GroupBy(x => x.SaleDate.ToString("MMMM"))
            .Select(k => new SpliteChartData()
            {
                Month = k.Key,
                Income = k.Sum(x => x.SaleItems.Sum(si => si.Quantity * si.UnitPrice))
            });

        var expenseTotal = _context.Supplies
            .Include(x => x.SupplyItems)
            .ToList()
            .GroupBy(x => x.SupplyDate.ToString("MMMM"))
            .Select(k => new SpliteChartData()
            {
                Month = k.Key,
                Expense = k.Sum(x => x.SupplyItems.Sum(si => si.Quantity * si.UnitPrice))
            });

        var lastYear = Enumerable.Range(0, 12)
            .Select(x => DateTime.Now.AddMonths(-x).ToString("MMMM"))
            .ToList();

        var data = from month in lastYear
                   join income in incomeTotal on month equals income.Month into joinedIncome
                   from income in joinedIncome.DefaultIfEmpty()
                   join expense in expenseTotal on month equals expense.Month into joinedExpense
                   from expense in joinedExpense.DefaultIfEmpty()
                   select new SpliteChartData()
                   {
                       Month = month,
                       Income = income?.Income ?? 0,
                       Expense = expense?.Expense ?? 0
                   };

        return data;
    }

    private IEnumerable<TransactionDto> GetTransactions()
    {
        var sales = _context.Sales
            .OrderByDescending(s => s.SaleDate)
            .Include(x => x.SaleItems)
            .Take(10)
            .AsNoTracking()
            .ToList();
        var supplies = _context.Supplies
            .OrderByDescending(s => s.SupplyDate)
            .Include(x => x.SupplyItems)
            .Take(10)
            .AsNoTracking()
            .ToList();

        List<TransactionDto> incomes = sales.Select(x => new TransactionDto
        {
            Id = x.Id,
            Type = "Income",
            Amount = x.SaleItems.Sum(si => si.Quantity * si.UnitPrice),
            Date = x.SaleDate
        }).ToList();

        List<TransactionDto> expenses = supplies.Select(x => new TransactionDto
        {
            Id = x.Id,
            Type = "Expense",
            Amount = x.SupplyItems.Sum(si => si.Quantity * si.UnitPrice),
            Date = x.SupplyDate
        }).ToList();

        return incomes.Concat(expenses).OrderByDescending(x => x.Date).Take(10);
    }
}
