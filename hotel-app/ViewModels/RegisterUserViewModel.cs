using hotel_app.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace hotel_app.ViewModels
{
    public class RegisterUserViewModel
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        //type changed to Iformfile
        [Display(Name = "Image URL")]
        public IFormFile photo { get; set; }
        public int StarRating { get; set; }
        public int Category { get; set; }
        //selected item
        public List<HotelCategory>? Categories { get; set; }
       
        [Required(ErrorMessage = "Please enter a user name.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        
    }
}
