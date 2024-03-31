using hotel_app.Repositories;
using hotel_app.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace hotel_app.ValidationModels
{
    public class UniqueUserName: ValidationAttribute
    {
        IGuestRepository guestRepository;

        public UniqueUserName(IGuestRepository _guestRepository)
        {
            guestRepository = _guestRepository;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string userName = value?.ToString();  // the value you want to validate
            UserProfileViewModel viewModel = validationContext.ObjectInstance as UserProfileViewModel;

           int count = guestRepository.getGuestByUserNameCount(userName);
            if(count == 0) // userName is a new unique username 
            {
                return ValidationResult.Success;
            }
            else
            {
                string name = guestRepository.getGuestNamebyId(viewModel.UserId);
                if (name == userName)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("name already taken");
            }
            
        }
    }
}
