using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BertLunch.Pages.PaymentPage
{
    [Authorize]
    public class PaymentSuccessfulModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
