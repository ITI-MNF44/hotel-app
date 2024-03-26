using hotel_app.Models;
using hotel_app.ViewModels;

namespace hotel_app.Repositories
{
    public interface IHotelRepository: IGeneralRepository<Hotel>
    {
        Task RegisterInsert(RegisterUserViewModel hotelvm);
        public Task<Hotel> GetHotelByUserId(string userId);
        public List<Room> getReservationsDetails(int hotelId);
        public List<GuestRoom> getRoomReservationsDetails(int id);
       
    }


}
