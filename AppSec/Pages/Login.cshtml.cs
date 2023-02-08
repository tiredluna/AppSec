using AppSec.ViewModels;
using AppSec.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace AppSec.Pages
{
    //Initialize the build-in ASP.NET Identity
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login LModel { get; set; }

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public LoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        //save data into database
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password,
                    LModel.RememberMe, lockoutOnFailure: true);
                if (identityResult.Succeeded)
                {
                    HttpContext.Session.SetString("Email", LModel.Email);
                    //Create the security context
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, "c@c.com"),
                        new Claim(ClaimTypes.Email, "c@c.com"),

                        new Claim("Department", "Admin")

                    };
                    var i = new ClaimsIdentity(claims, "MyCookieAuth");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(i);
                    await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
                    return RedirectToPage("Index");
                }

                if(identityResult.IsLockedOut)
                {
                    ModelState.AddModelError("", "Your account is locked out, please wait for 5 minutes before attempting to login again");
                }
                ModelState.AddModelError("", "Invalid Username or Password");
            }
            return Page();
        }
    }
}
