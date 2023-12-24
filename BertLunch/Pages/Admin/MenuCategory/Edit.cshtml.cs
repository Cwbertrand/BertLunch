using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BertLunch.Pages.Admin.MenuCategory
{
    public class EditModel : PageModel
    {
        private readonly Model.Data.BertLunchContext _context;

        public EditModel(Model.Data.BertLunchContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Model.MenuCategory MenuCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.MenuCategories == null)
            {
                return NotFound();
            }

            var menucategory =  await _context.MenuCategories.FirstOrDefaultAsync(m => m.Id == id);
            if (menucategory == null)
            {
                return NotFound();
            }
            MenuCategory = menucategory;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MenuCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuCategoryExists(MenuCategory.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MenuCategoryExists(int id)
        {
          return (_context.MenuCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
