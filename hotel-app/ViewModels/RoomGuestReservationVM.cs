using hotel_app.Models;

namespace hotel_app.ViewModels
{
    public class RoomGuestReservationVM
    {
        public string Full_name { get; set; }
        public int Rooms_amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime  EndDate{ get; set; }
        public DateTime BookingDate { get; set; }
        public decimal RoomPrice { get; set; }
        public string? FoodCategory { get; set; }
        public decimal? FoodPrice { get; set; }
        public string? FoodName { get; set; }
        public int? FoodAmount { get; set; }

    }

}
