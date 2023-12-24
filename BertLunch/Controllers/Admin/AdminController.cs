using Microsoft.AspNetCore.Mvc;

namespace BertLunch.Controllers.Admin
{
    public class AdminController : Controller
    {
        [Route("admin")]
        public IActionResult Index()
        {
            return View("../../Pages/Admin/Index");
        }
    }
}