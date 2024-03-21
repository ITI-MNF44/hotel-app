using hotel_app.Models;

namespace hotel_app.Repositories
{
    public interface IRoomRepository:IGeneralRepository<Room>
    {
        public void test_function();
    }
}
