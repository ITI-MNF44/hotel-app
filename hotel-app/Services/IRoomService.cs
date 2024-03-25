using hotel_app.Models;
using hotel_app.Repositories;

namespace hotel_app.Services
{
    public interface IRoomService : IGeneralRepository<Room>
    {

    }
    //public interface IRoomServices : IGeneralRepository<Room>
    //{
    //    public void test_function();
    //}
}
