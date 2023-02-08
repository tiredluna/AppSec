using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppSec.Pages
{
    [Authorize(Policy = "MustBelongToAdminDepartment")]
    public class AdminModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
