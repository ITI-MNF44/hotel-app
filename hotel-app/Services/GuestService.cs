using hotel_app.Models;
using hotel_app.Repositories;
using hotel_app.ViewModels;
using System.Security.Claims;

namespace hotel_app.Services
{
    public class GuestService : IGuestService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        HttpContext httpContext;
        IGuestRepository GuestRepository;
        public GuestService(IHttpContextAccessor _httpContextAccessor, IGuestRepository _guestRepository)
        {
            httpContextAccessor = _httpContextAccessor;
            httpContext = httpContextAccessor.HttpContext;
            GuestRepository = _guestRepository;
        }
        public async Task<Guest> GetCurrentGuest()
        {
            var claimsIdentity = httpContext.User.Identity as ClaimsIdentity;
            // Find the claim representing the user ID
            var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            // return hotel
            return await GuestRepository.GetGuestByUserId(userIdClaim.Value);
        }
        public ApplicationUser MapGuestVmToAppUser(RegisterGuestViewModel guestVM)
        {
            return new ApplicationUser() { UserName = guestVM.UserName, PasswordHash = guestVM.Password };
        }
        public Guest MapGuestVmToGuest(RegisterGuestViewModel guestVM)
        {
            return new Guest() { 
                FirstName = guestVM.FirstName,
                LastName = guestVM.LastName,
                BirthDate = new DateTime(guestVM.BirthDate.Year, guestVM.BirthDate.Month, guestVM.BirthDate.Day),
                Gender = guestVM.Gender,
                Country = guestVM.Country,
                CreatedDate = DateTime.Now,
                isDeleted=false
            };
        }

        public void InsertGuest(Guest guest)
        {
            GuestRepository.Insert(guest);
        }
        public void SaveChanges()
        {
            GuestRepository.Save();
        }


    }
}
