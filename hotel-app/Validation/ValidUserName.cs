using hotel_app.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace hotel_app.Validation
{
    public class ValidUserName : ValidationAttribute
    {
       

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            UserManager<ApplicationUser> _usermanager = validationContext.GetService<UserManager<ApplicationUser>>();



            var dbUser = _usermanager.FindByNameAsync(value.ToString());

            if (dbUser != null)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
