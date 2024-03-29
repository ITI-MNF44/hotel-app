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

        public List<RoomGuestReservationVM> getGuestReservations(int guest_id)
        {
            return GuestRepository.getGuestReservations(guest_id).Select(x => new RoomGuestReservationVM()
            {
                roomName = x.Room.Name,
                roomCategory = x.Room.RoomCategory.Name,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                BookingDate = x.BookingDate,
                Rooms_amount = x.RoomsAmount,
                RoomPrice = x.Room.PricePerNight,
                FoodAmount = x.FoodAmount,
                FoodCategory = x.Food == null ? null : x.Food.Category.Name,
                FoodName = x.Food == null ? null : x.Food.Name,
                FoodPrice = x.Food == null ? null : x.Food.PricePerPerson,

            }).ToList();
        }

        public UserProfileViewModel GetUserProfile(int guest_id)
        {
           var guest = GuestRepository.getGuestDetails(guest_id);
            return new UserProfileViewModel()
            {
                UserId = guest.User.Id,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                BirthDate = guest.BirthDate,
                UserName = guest.User.UserName,
                UserPhone = guest.User.PhoneNumber,
                UserEmail = guest.User.Email,
                Password = guest.User.PasswordHash,

            };
        }

        public int getGuestByUserNameCount(string userName)
        {
            return GuestRepository.getGuestByUserNameCount(userName);
        }

        public string getGuestUserNameById(string id)
        {
            return GuestRepository.getGuestNamebyId(id);
        }
    }
}
