using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;

namespace BertLunch.Pages.ProfileView.Account.Manage
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public AppUser BertUser { get; set; }
        public ProfileModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task OnGetAsync()
        {
            BertUser = await _userManager.GetUserAsync(User);
        }
    }
}
