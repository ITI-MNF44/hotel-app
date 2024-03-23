using hotel_app.Models;

namespace hotel_app.Repositories
{
    public class RoomCategoryRepository : GeneralRepository<RoomCategory>, IRoomCategoryRepository
    {
        public RoomCategoryRepository(dbContext DbContext) : base(DbContext)
        {
        }
    }
}
