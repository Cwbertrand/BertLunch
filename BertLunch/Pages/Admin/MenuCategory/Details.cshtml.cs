using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BertLunch.Pages.Admin.MenuCategory
{
    public class DetailsModel : PageModel
    {
        private readonly Model.Data.BertLunchContext _context;

        public DetailsModel(Model.Data.BertLunchContext context)
        {
            _context = context;
        }

      public Model.MenuCategory MenuCategory { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.MenuCategories == null)
            {
                return NotFound();
            }

            var menucategory = await _context.MenuCategories.FirstOrDefaultAsync(m => m.Id == id);
            if (menucategory == null)
            {
                return NotFound();
            }
            else 
            {
                MenuCategory = menucategory;
            }
            return Page();
        }
    }
}
