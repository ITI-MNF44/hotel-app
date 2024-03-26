using hotel_app.Models;
using System.ComponentModel.DataAnnotations;

namespace hotel_app.ViewModels
{
    public class RegisterGuestViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Gender { get; set; }
        public DateOnly BirthDate { get; set; }
        public string? Country { get; set; }
    }
}
