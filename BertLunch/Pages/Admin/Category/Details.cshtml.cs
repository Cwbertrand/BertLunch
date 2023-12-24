using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BertLunch.Pages.Admin.Category
{
    public class DetailsModel : PageModel
    {
        private readonly Model.Data.BertLunchContext _context;

        public DetailsModel(Model.Data.BertLunchContext context)
        {
            _context = context;
        }

      public Model.Category Category { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            else 
            {
                Category = category;
            }
            return Page();
        }
    }
}
