using hotel_app.Models;

namespace hotel_app.Repositories
{
    public class FoodRepository : GeneralRepository<Food>, IFoodRepository
    {
        public FoodRepository(HotelDbContext DbContext) : base(DbContext)
        {
        }

        public Food test_function()
        {
            throw new NotImplementedException();
        }
    }
}
