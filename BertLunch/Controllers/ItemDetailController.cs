using BertLunch.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Data;

namespace BertLunch.Controllers
{
    [Route("detail")]
    public class ItemDetailController : Controller
    {
        private readonly BertLunchContext _context;

        public ItemDetailController(BertLunchContext context)
        {
            _context = context;
        }

        [HttpGet("item/{itemId}")]
        public async Task<IActionResult> GetMenuItemDetails(int itemId)
        {
            var menuItem = await _context.MenuItem
                .Include(x => x.Category)
                .Include(x => x.MenuCategory)
                .SingleOrDefaultAsync(x => x.Id == itemId);

            if (menuItem == null)
            {
                return NotFound();
            }

            return new JsonResult(new
            {
                menuName = menuItem.MenuName,
                price = menuItem.Price.ToString("F2"),
                description = menuItem.Description,
                ingredient = menuItem.Ingredient,
                image = menuItem.Image
            });
        }
    }
}
