using BertLunch.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BertLunch.Pages.PaymentPage
{
    public class IndexModel : PageModel
    {
        private readonly BasketService _basketService;

        public IndexModel(BasketService basketService)
        {
            _basketService = basketService;
        }
        public void OnGet()
        {
            ViewData["CartItems"] = _basketService.GetBasket();
        }
    }
}
