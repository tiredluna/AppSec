using System.ComponentModel.DataAnnotations;

namespace AppSec.ViewModels
{
    public class Register
    {
        [Required, MinLength(3, ErrorMessage = "Please enter at least 3 characters."), MaxLength(50)]
        [DataType(DataType.Text)]
        public string FullName { get; set; }

        [Required, MinLength(3, ErrorMessage = "Please enter a 16 digit credit card number."), MaxLength(16)]
        [DataType(DataType.CreditCard)]
        public string CreditCardNo { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string MobileNo { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string DeliveryAddress { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirm password does not match")]
        public string ConfirmPassword { get; set; }

        public string ImageURL { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.MultilineText)]
        public string AboutMe { get; set; }
    }
}
