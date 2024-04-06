using hotel_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace hotel_app.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a country.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please enter a city.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter an address.")]
        public string Address { get; set; }

        //
        [Required(ErrorMessage = "Please upload an image.")]
        [Display(Name = "Upload Image")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Please select a star rating.")]
        [Display(Name = "Star Rating")]

        public int StarRating { get; set; }

        //[Required(ErrorMessage = "Please select a category.")]
        public int Category { get; set; }
        public List<HotelCategory>? Categories { get; set; }

        [Required(ErrorMessage = "Please enter a user name.")]
        [Display(Name = "User Name")]
        [Remote("CheckNameAvailability", "Guest", ErrorMessage = "username is not available")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Remote("CheckEmailAvailability", "Guest", ErrorMessage = "Email already in use")]

        public string Email { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must have at least one uppercase ('A'-'Z'), one lowercase ('a'-'z'), and one digit (0-9).")]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password must be at least 6 characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

    }
}
