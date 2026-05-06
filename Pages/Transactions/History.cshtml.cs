using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using PersonalFinanceTracker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalFinanceTracker.Pages.Transactions
{
    public class HistoryModel : PageModel
    {
        private readonly IMongoCollection<Transaction> _transactions;
        public List<Transaction> Transactions { get; set; } = new();

        public HistoryModel(IMongoDatabase database)
        {
            _transactions = database.GetCollection<Transaction>("Transactions");
        }

        public async Task OnGetAsync()
        {
            Transactions = await _transactions.Find(_ => true)
                                              .SortByDescending(t => t.Date)
                                              .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            await _transactions.DeleteOneAsync(t => t.Id == id);
            return RedirectToPage();
        }
    }
}