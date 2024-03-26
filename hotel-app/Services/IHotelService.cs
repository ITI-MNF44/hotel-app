using hotel_app.Models;
using hotel_app.ViewModels;

namespace hotel_app.Services
{
    public interface IHotelService
    {
        public Task<Hotel> GetCurrentHotel();

        public List<Room> ReservationsInfo(int id);
        public List<RoomGuestReservationVM> RoomReservationsDetails(int id);

        //hotel register
        public Task RegisterInsert(RegisterUserViewModel hotelvm);

    }
}
