using hotel_app.Repositories;
using hotel_app.ViewModels;

namespace hotel_app.Services
{
    public class HotelService:IHotelService
    {
        IHotelRepository _hotelRepository;

        public HotelService(IHotelRepository hotelRepository) 
        {
            _hotelRepository = hotelRepository;
        }

        //Register Hotel
        public async Task RegisterInsert(RegisterUserViewModel hotelvm)
        {
           await _hotelRepository.RegisterInsert(hotelvm);
        }
        


    }
}
