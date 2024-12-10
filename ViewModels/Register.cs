using System.ComponentModel.DataAnnotations;
using WebClothes.Areas.User.CustomValidation;

namespace WebClothes.ViewModels
{
    public class Register
    {
        [Required]
        [EmailAddress] 
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [NoAlphaAndDigit]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")] 
        public string ConfirmPassword { get; set; }
    }
}
