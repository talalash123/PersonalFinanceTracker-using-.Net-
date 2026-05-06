using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Pages.Settings
{
    public class CategoriesModel : PageModel
    {
        private readonly IMongoCollection<Category> _categories;

        public CategoriesModel(IMongoDatabase database)
        {
            _categories = database.GetCollection<Category>("Categories");
        }

        public List<Category> CategoryList { get; set; } = new();

        [BindProperty]
        public Category NewCategory { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Database se sari categories load karna
            CategoryList = await _categories.Find(_ => true).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(NewCategory.Name)) return Page();

            // Nayi category save karna
            await _categories.InsertOneAsync(NewCategory);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            // Category delete karna
            await _categories.DeleteOneAsync(c => c.Id == id);
            return RedirectToPage();
        }
    }
}