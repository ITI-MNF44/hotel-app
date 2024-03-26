using hotel_app.Models;

namespace hotel_app.Repositories
{
    public interface IHotelRepository: IGeneralRepository<Hotel>
    {
        public void test_func();
        public Task<Hotel> GetHotelByUserId(string userId);
    }


}
