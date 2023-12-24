using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Data;

namespace BertLunch.Pages.Admin.MenuItem
{
    public class IndexModel : PageModel
    {
        private readonly Model.Data.BertLunchContext _context;

        public IndexModel(Model.Data.BertLunchContext context)
        {
            _context = context;
        }

        public IList<Model.MenuItem> MenuItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.MenuItem != null)
            {
                MenuItem = await _context.MenuItem
                .Include(m => m.Category)
                .Include(m => m.MenuCategory).ToListAsync();
            }
        }
    }
}
