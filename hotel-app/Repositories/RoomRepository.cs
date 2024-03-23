using hotel_app.Models;

namespace hotel_app.Repositories
{
    public class RoomRepository : GeneralRepository<Room>, IRoomRepository
    {
        public RoomRepository(dbContext DbContext) : base(DbContext)
        {
        }
        public void test_function()
        {
            throw new NotImplementedException();
        }
    }
}
