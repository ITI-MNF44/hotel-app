using hotel_app.Models;
using System.ComponentModel.DataAnnotations;

namespace hotel_app.ViewModels
{
    public class BookingDetailsViewModel
    {
        public int RoomId { get; set; }
        public string Image { get; set; }
        public string RoomName{ get; set; }
        public string HotelName{ get; set; }
        public string Country{ get; set; }
        public string City{ get; set; }
        public string RoomCategory{ get; set; }
        public int BedsNum{ get; set; }
        public string RoomDescription{ get; set; }
        public List<Food>? FoodList{ get; set; }
        public int? FoodId{ get; set; }
        public decimal price{ get; set; }
        [DataType(DataType.DateTime)]
        public DateTime StartDate{ get; set; }
        [DataType(DataType.DateTime)]
        public DateTime EndDate{ get; set; }
        public int Amount{ get; set; }
        
        public int RoomsAmount{ get; set; }

        
    }
}
