using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BertLunch.Pages.PaymentPage
{
    [Authorize]
    public class PaymentFailureModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
