using AppSec.Model;
using AppSec.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AppSec.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private UserManager<ApplicationUser> userManager { get; }

        public IndexModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [BindProperty]
        public ApplicationUser user { get; set; } = new();
        public string sessionValue { get; set; }
        public async Task<IActionResult> OnGet()
        {
            sessionValue = HttpContext.Session.GetString("Email");
            if (sessionValue.IsNullOrEmpty())
            {
                await signInManager.SignOutAsync();
                HttpContext.Session.Clear();
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Session timeout, please login again.");
                return RedirectToPage("Login");
            }

            //decrypt creditcard
            var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
            var protector = dataProtectionProvider.CreateProtector("MySecretKey");
            user = await userManager.GetUserAsync(User);
            user.CreditCardNo = protector.Unprotect(user.CreditCardNo);

            //decode aboutme
            var decode = Convert.FromBase64String(user.AboutMe);
            user.AboutMe = Encoding.UTF8.GetString(decode);

            return Page();
        }
    }
}