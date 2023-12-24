using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BertLunch.Pages.Menu
{
    public class MenuItemModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }
        public List<IGrouping<int, MenuItem>> MenuItems { get; set; }
        //public List<IGrouping<int, MenuItem>> Burger { get; set; }
        private readonly BertLunchContext _context;

        public MenuItemModel(BertLunchContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            if (!CategoryId.HasValue)
            {
                CategoryId = 3;
            };

            IQueryable<MenuItem> query = _context.MenuItem
                .Include(x => x.Category)
                .Include(x => x.MenuCategory);

            if (CategoryId.HasValue)
            {
                query = query.Where(x => x.MenuCategoryId == CategoryId.Value);
            }

            List<MenuItem> items = await query.ToListAsync();
            MenuItems = items.GroupBy(x => x.Category.Id).ToList();
        }
    }
}
