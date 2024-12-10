using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;


namespace WebClothes.Areas.User.CustomValidation
{
  
    public class NoAlphaAndDigitAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var password = value.ToString();
                if (!Regex.IsMatch(password, @"\d") || !Regex.IsMatch(password, @"\W"))
                {
                    return new ValidationResult("Password must contain at least one non-alphabetic character and one digit.");
                }
            }
            return ValidationResult.Success;
        }
    }

}
