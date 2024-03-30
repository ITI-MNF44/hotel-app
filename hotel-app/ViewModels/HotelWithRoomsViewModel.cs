using hotel_app.Models;

namespace hotel_app.ViewModels
{
    public class HotelWithRoomsViewModel
    {
        public Hotel Hotel { get; set; }
        public List<Room> Rooms { get; set; }
        public HotelCategory HotelCategory { get; set; }

        public  RoomCategory RoomCategory { get; set; }
    }
}
