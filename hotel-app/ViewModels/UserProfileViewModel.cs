using hotel_app.ValidationModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace hotel_app.ViewModels
{
    public class UserProfileViewModel
    {
        public string? UserId { get; set; }
        public int? guestId { get; set; }

        [Required(ErrorMessage = "user name is required")]
        [RegularExpression("^[a-zA-Z]{2}[a-zA-Z0-9]*$", ErrorMessage = "name must start with 2 char followed by numbers and and chars only")]
        [Remote("validateUserName", "Guest", ErrorMessage = "name already exist", AdditionalFields = "UserId")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "this field is required")]
        [RegularExpression("^[a-zA-Z]{2}[a-zA-Z0-9]*$", ErrorMessage = "name must start with 2 char followed by numbers and and chars only")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "this field is required")]
        [RegularExpression("^[a-zA-Z]{2}[a-zA-Z0-9]*$", ErrorMessage = "name must start with 2 char followed by numbers and and chars only")]
        public string? LastName { get; set; }

        [RegularExpression(@"^[a-zA-Z]{2}[a-zA-Z0-9]*@(gmail|yahoo)\.com$", ErrorMessage = "Name must start with 2 characters followed by numbers and characters only.")]
        [Remote("validateEmail", "Guest", ErrorMessage = "email already exist", AdditionalFields = "UserId")]
        public string? UserEmail { get; set; }


        [RegularExpression(@"^(?:\d{11}|)$", ErrorMessage = "Number must be 11 digits")]
        public string? UserPhone { get; set; }

        [Required]
        public DateTime? BirthDate { get; set; }


        [Remote("ValidateCurrentPassword", "Guest", ErrorMessage = "wrong pass", AdditionalFields = "UserId")]
        public string? Password { get; set; }


        [Required(ErrorMessage ="this field is reauired")]
        [RegularExpression("^([a-zA-Z0-9]{5,}.*)?$", ErrorMessage = "Password should start with at least 5 alphanumeric characters.")]
        public string? NewPassword { get; set; }


        [Required(ErrorMessage = "this field is reauired")]
        [Remote("validateConfirmPassword", "Guest", ErrorMessage = "password is not identical", AdditionalFields = "NewPassword")]
        public string? ConfirmPassword { get; set; }

        public override string ToString()
        {
            return "BD " + BirthDate+"\n"+"user name "+UserName;
        }
    }
    }