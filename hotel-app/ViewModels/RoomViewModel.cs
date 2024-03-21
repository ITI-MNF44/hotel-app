namespace hotel_app.ViewModels
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HotelName { get; set; }
        public string CategoryName { get; set; }
        public int NumOfBeds { get; set; }
        public decimal PricePerNight { get; set; }
        public int NumOfRooms { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public IFormFile Image { get; set; }
    }
}
