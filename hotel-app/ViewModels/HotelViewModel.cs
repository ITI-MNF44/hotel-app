namespace hotel_app.ViewModels
{
    public class HotelViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public IFormFile Image { get; set; }
        public int StarRating { get; set; }
        public string Category { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsAccessible { get; set; }
        public bool? IsWorking { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsDeleted { get; set; }
    }
}
