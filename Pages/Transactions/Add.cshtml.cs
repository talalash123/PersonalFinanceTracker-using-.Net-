using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Pages.Transactions
{
    public class AddModel : PageModel
    {
        private readonly IMongoCollection<Transaction> _transactions;
        private readonly IMongoCollection<Category> _categories; // Category collection ka izafa

        public AddModel(IMongoDatabase database)
        {
            _transactions = database.GetCollection<Transaction>("Transactions");
            _categories = database.GetCollection<Category>("Categories"); // Initialize
        }

        [BindProperty]
        public Transaction Transaction { get; set; } = new();

        // Dropdown ke liye list property
        public List<Category> AvailableCategories { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Database se sari categories utha kar list mein daal dena
            AvailableCategories = await _categories.Find(_ => true).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Agar error aaye to dobara categories load karni parhengi
                AvailableCategories = await _categories.Find(_ => true).ToListAsync();
                return Page();
            }

            Transaction.Date = DateTime.Now;
            await _transactions.InsertOneAsync(Transaction);

            return RedirectToPage("/Transactions/History");
        }
    }
}