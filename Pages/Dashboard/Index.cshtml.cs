using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using PersonalFinanceTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinanceTracker.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly IMongoCollection<Transaction> _transactions;

        public IndexModel(IMongoDatabase database)
        {
              _transactions = database.GetCollection<Transaction>("Transactions");
            }

            public decimal TotalIncome { get; set; }
            public decimal TotalExpense { get; set; }
            public decimal NetBalance { get; set; }
            public List<Transaction> RecentTransactions { get; set; } = new();

            public async Task OnGetAsync()
            {
                // MongoDB se data fetch karna
                var allTransactions = await _transactions.Find(_ => true).ToListAsync();

                // Real-time calculations
                TotalIncome = allTransactions.Where(t => !t.IsExpense).Sum(t => t.Amount);
                TotalExpense = allTransactions.Where(t => t.IsExpense).Sum(t => t.Amount);
                NetBalance = TotalIncome - TotalExpense;

                // Latest 5 entries fetch karna
                RecentTransactions = allTransactions
                    .OrderByDescending(t => t.Date)
                    .Take(5)
                    .ToList();
            }
        }
    }