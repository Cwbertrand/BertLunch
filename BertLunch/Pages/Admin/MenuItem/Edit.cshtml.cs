using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BertLunch.Pages.Admin.MenuItem
{
    public class EditModel : PageModel
    {
        private readonly Model.Data.BertLunchContext _context;

        public EditModel(Model.Data.BertLunchContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Model.MenuItem MenuItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.MenuItem == null)
            {
                return NotFound();
            }

            var menuitem =  await _context.MenuItem.FirstOrDefaultAsync(m => m.Id == id);
            if (menuitem == null)
            {
                return NotFound();
            }
            MenuItem = menuitem;
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Label");
            ViewData["MenuCategoryId"] = new SelectList(_context.MenuCategories, "Id", "Name");
            return Page();
        }
    }
}
