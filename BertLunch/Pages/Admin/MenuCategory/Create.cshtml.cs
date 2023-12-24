using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BertLunch.Pages.Admin.MenuCategory
{
    public class CreateModel : PageModel
    {
        private readonly Model.Data.BertLunchContext _context;

        public CreateModel(Model.Data.BertLunchContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Model.MenuCategory MenuCategory { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          //if (!ModelState.IsValid || _context.MenuCategories == null || MenuCategory == null)
          //  {
          //      return Page();
          //  }

            _context.MenuCategories.Add(MenuCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
