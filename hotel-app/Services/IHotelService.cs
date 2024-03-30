using hotel_app.Models;
using hotel_app.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace hotel_app.Services
{
    public interface IHotelService
    {
        public Task<Hotel> GetCurrentHotel();

        public List<Room> ReservationsInfo(int id);
        public List<Hotel> AllHotels();
        public List<RoomGuestReservationVM> RoomReservationsDetails(int id);

        //hotel services
        public ApplicationUser MapHotelUserVmToAppUser(RegisterUserViewModel hotelvm);
        public  Task<Hotel> MapHotelVmToHotel(RegisterUserViewModel hotelvm, string userId);
        public Task RegisterInsert(Hotel hotel);


        public HotelWithRoomsViewModel GetHotelWithRooms(int hotelId);
        public HotelWithRoomsViewModel MAPHotelRoomsVM(Hotel hotel, List<Room> rooms, HotelCategory hotelCategory, RoomCategory roomCategory);
    }
}
