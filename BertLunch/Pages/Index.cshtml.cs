using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Data;

namespace BertLunch.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BertLunchContext _context;

        public List<MenuItem> MenuItem { get; set; }
        public List<Review> Reviews { get; set; }
        
        public IndexModel(BertLunchContext context)
        {
            _context = context;
        }


        public async Task OnGetAsync()
        {
            if (_context.MenuItem != null)
            {
                MenuItem = await _context.MenuItem
                        .Include(x => x.Category)
                        .Include(x => x.MenuCategory)
                        .Where(x => x.CategoryId == 8)
                        .Take(3).ToListAsync();
            }

            if (_context.Reviews != null)
            {
                Reviews = await _context.Reviews
                    .Include(x => x.User)
                    .Take(3).ToListAsync();
            }

        }
    }
}