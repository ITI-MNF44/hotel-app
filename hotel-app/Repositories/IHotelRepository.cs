using hotel_app.Models;

namespace hotel_app.Repositories
{
    public interface IHotelRepository: IGeneralRepository<Hotel>
    {
        public Task<Hotel> GetHotelByUserId(string userId);
        public List<Hotel> AllHotels();
        public List<Room> getReservationsDetails(int hotelId);
        public List<GuestRoom> getRoomReservationsDetails(int id);
    }


}
