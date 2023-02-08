using AppSec.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace AppSec.Pages
{
    [Authorize(Roles = "Admin")]
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private readonly SignInManager<ApplicationUser> signInManager;

        public PrivacyModel(ILogger<PrivacyModel> logger, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            var sessionValue = HttpContext.Session.GetString("Email");
            if (sessionValue.IsNullOrEmpty())
            {
                await signInManager.SignOutAsync();
                HttpContext.Session.Clear();
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Session timeout, please login again");
                return RedirectToPage("Login");
            }
            return Page();
        }
    }
}