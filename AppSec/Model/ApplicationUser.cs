using Microsoft.AspNetCore.Identity;

namespace AppSec.Model
{
    public class ApplicationUser : IdentityUser
    {

        public string FullName { get; set; } = string.Empty;

        public string CreditCardNo { get; set; }

        public string Gender { get; set; } = string.Empty;

        public string MobileNo { get; set; }

        public string DeliveryAddress { get; set; } = string.Empty;

        public string ImageURL { get; set; }

        public string AboutMe { get; set; } = string.Empty;
    }
}
