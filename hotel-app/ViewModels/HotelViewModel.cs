using hotel_app.Models;

namespace hotel_app.ViewModels
{
    public class HotelViewModel
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int StarRating { get; set; }
        public int Category { get; set; }
        public DateTime? CreatedDate { get; set; }
     
    }
}
