using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BertLunch.Pages.Admin.MenuCategory
{
    public class IndexModel : PageModel
    {
        private readonly Model.Data.BertLunchContext _context;

        public IndexModel(Model.Data.BertLunchContext context)
        {
            _context = context;
        }

        public IList<Model.MenuCategory> MenuCategory { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.MenuCategories != null)
            {
                MenuCategory = await _context.MenuCategories.ToListAsync();
            }
        }
    }
}
