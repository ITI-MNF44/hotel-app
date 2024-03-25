using hotel_app.Models;
using hotel_app.ViewModels;

namespace hotel_app.Services
{
    public interface IGuestService
    {
        public Task<Guest> GetCurrentGuest();
        public ApplicationUser MapGuestVmToAppUser(RegisterGuestViewModel guest);

        public Guest MapGuestVmToGuest(RegisterGuestViewModel guestVM);

        public void InsertGuest(Guest guest);

        public void SaveChanges();


    }
}
