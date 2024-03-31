using hotel_app.Models;
using hotel_app.ViewModels;

namespace hotel_app.Repositories
{
    public interface IGuestRepository: IGeneralRepository<Guest>
    {
        public Task<Guest> GetGuestByUserId(string userId);

        public List<GuestRoom> getGuestReservations(int id);

        public Guest getGuestDetails(int id);
        public int getGuestByUserNameCount(string guestUserName);
        public string getGuestNamebyId(string id);

        public  void editGuestProfile(UserProfileViewModel user);
    }
}
