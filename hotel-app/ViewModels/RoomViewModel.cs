using hotel_app.Models;
using System.ComponentModel.DataAnnotations;

namespace hotel_app.ViewModels
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? HotelName { get; set; }
        public string? CategoryName { get; set; }
        public int HotelId { get; set; }
        public int Category { get; set; }
        [Display(Name = "Number Of Beds")]
        public int NumOfBeds { get; set; }
        [Display(Name = "Price Per Night")]
        public decimal PricePerNight { get; set; }
        [Display(Name = "Number Of Rooms")]
        public int NumOfRooms { get; set; }
        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "Is Deleted")]
        public bool IsDeleted { get; set; }
        public IFormFile? Image { get; set; }
        public List<RoomCategory>? Categories { get; set; } = new List<RoomCategory> { };
        
        }
}
