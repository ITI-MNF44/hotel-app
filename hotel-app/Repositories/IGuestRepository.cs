using hotel_app.Models;

namespace hotel_app.Repositories
{
    public interface IGuestRepository: IGeneralRepository<Guest>
    {
        public Task<Guest> GetGuestByUserId(string userId);

        public List<GuestRoom> getGuestReservations(int id);

        public Guest getGuestDetails(int id);
        public int getGuestByUserNameCount(string guestUserName);
        public string getGuestNamebyId(string id);

    }
}
