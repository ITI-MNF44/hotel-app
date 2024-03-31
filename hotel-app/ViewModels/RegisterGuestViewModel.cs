using hotel_app.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace hotel_app.ViewModels
{
    public class RegisterGuestViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]

        public string Email { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z]).{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter.")]

        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="confirm password is not similar to password")]
        public string ConfirmPassword { get; set; }


        public string Gender { get; set; }
        public DateOnly BirthDate { get; set; }
        [MinLength(1, ErrorMessage = "Country is required")]
        public string Country { get; set; }

        public List<string>? Countries { get; set; }
    }
}
