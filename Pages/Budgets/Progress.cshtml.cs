using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Pages.Budgets
{
    public class ProgressModel : PageModel
    {
        private readonly IMongoCollection<Transaction> _transactions;

        public ProgressModel(IMongoDatabase database)
        {
            _transactions = database.GetCollection<Transaction>("Transactions");
        }

        public List<SpendingCategory> SpendingList { get; set; } = new();
        public decimal TotalSpent { get; set; }
        public string CurrentFilter { get; set; } = "monthly";

        public async Task OnGetAsync(string filter = "monthly")
        {
            CurrentFilter = filter;
            var now = DateTime.Now;

            // Filter logic based on time periods
            DateTime startDate = filter switch
            {
                "3days" => now.AddDays(-3),
                "7days" => now.AddDays(-7),
                "yearly" => new DateTime(now.Year, 1, 1),
                _ => new DateTime(now.Year, now.Month, 1) // Default: Monthly
            };

            // Fetch only Expenses within the date range
            var transactions = await _transactions
                .Find(t => t.IsExpense == true && t.Date >= startDate)
                .ToListAsync();

            TotalSpent = transactions.Sum(t => t.Amount);

            if (TotalSpent > 0)
            {
                // Grouping by Category and calculating percentage share
                SpendingList = transactions
                    .GroupBy(t => t.Category)
                    .Select(g => new SpendingCategory
                    {
                        CategoryName = g.Key,
                        Amount = g.Sum(t => t.Amount),
                        Percentage = (double)(g.Sum(t => t.Amount) / TotalSpent) * 100
                    })
                    .OrderByDescending(x => x.Amount)
                    .ToList();
            }
        }
    }

    public class SpendingCategory
    {
        public string CategoryName { get; set; } = "";
        public decimal Amount { get; set; }
        public double Percentage { get; set; }
    }
}