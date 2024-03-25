using hotel_app.Models;

namespace hotel_app.Repositories
{
    public class HotelCategoryRepository : GeneralRepository<HotelCategory>, IHotelCategoryRepository
    {
        public HotelCategoryRepository(HotelDbContext DbContext) : base(DbContext)
        {
        }
    }
}
