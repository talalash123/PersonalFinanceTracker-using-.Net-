using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Pages.Transactions
{
    public class DeleteModel : PageModel
    {
        private readonly MongoDbContext _context;

        public DeleteModel(MongoDbContext context) => _context = context;

        [BindProperty]
        public Transaction Transaction { get; set; } = default!;

        // Page load hote hi ID ke zariye transaction dhundna
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            Transaction = await _context.Transactions
                .Find(t => t.Id == id)
                .FirstOrDefaultAsync();

            if (Transaction == null) return NotFound();
            return Page();
        }

        // Form submit hone par MongoDB se record delete karna
        public async Task<IActionResult> OnPostAsync()
        {
            if (Transaction == null || string.IsNullOrEmpty(Transaction.Id)) return NotFound();

            await _context.Transactions.DeleteOneAsync(t => t.Id == Transaction.Id);

            return RedirectToPage("./History");
        }
    }
}