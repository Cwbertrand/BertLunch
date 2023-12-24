using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BertLunch.Pages.Admin.Category
{
    public class IndexModel : PageModel
    {
        private readonly Model.Data.BertLunchContext _context;

        public IndexModel(Model.Data.BertLunchContext context)
        {
            _context = context;
        }

        public IList<Model.Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Category != null)
            {
                Category = await _context.Category.ToListAsync();
            }
        }
    }
}
