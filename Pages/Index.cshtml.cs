using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PersonalFinanceTracker.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Jab koi khali URL (/) open kare, toh usey Dashboard folder mein bhejo
            return RedirectToPage("/Dashboard/Index");
        }
    }
}