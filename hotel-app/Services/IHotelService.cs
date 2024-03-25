using hotel_app.ViewModels;

namespace hotel_app.Services
{
    public interface IHotelService
    {
        public Task RegisterInsert(RegisterUserViewModel hotelvm);
    }
}
