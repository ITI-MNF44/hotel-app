using hotel_app.Models;
using hotel_app.ViewModels;

namespace hotel_app.Repositories
{
    public interface IHotelRepository: IGeneralRepository<Hotel>
    {
        Task RegisterInsert(Hotel hotel);
        public Task<Hotel> GetHotelByUserId(string userId);
        public List<Hotel> AllHotels();
        public List<Room> getReservationsDetails(int hotelId);
        public List<GuestRoom> getRoomReservationsDetails(int id);
       
    }


}
