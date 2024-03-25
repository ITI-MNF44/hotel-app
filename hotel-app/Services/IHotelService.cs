using hotel_app.Models;

namespace hotel_app.Services
{
    public interface IHotelService
    {
        public Task<Hotel> GetCurrentHotel();

    }
}
