using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Pages.Transactions
{
    public class EditModel : PageModel
    {
        private readonly IMongoCollection<Transaction> _transactions;

        public EditModel(IMongoDatabase database)
        {
            _transactions = database.GetCollection<Transaction>("Transactions");
        }

        [BindProperty]
        public Transaction Transaction { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return RedirectToPage("/Transactions/History");

            // Purana record fetch karna
            Transaction = await _transactions.Find(t => t.Id == id).FirstOrDefaultAsync();

            if (Transaction == null) return RedirectToPage("/Transactions/History");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // MongoDB mein purane data ko naye data se badal dena
            await _transactions.ReplaceOneAsync(t => t.Id == Transaction.Id, Transaction);

            return RedirectToPage("/Transactions/History");
        }
    }
}