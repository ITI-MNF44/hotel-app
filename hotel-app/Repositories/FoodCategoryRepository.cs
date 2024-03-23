using hotel_app.Models;

namespace hotel_app.Repositories
{
    public class FoodCategoryRepository : GeneralRepository<FoodCategory>, IFoodCategoryRepository
    {
        public FoodCategoryRepository(dbContext DbContext) : base(DbContext)
        {
        }

        public void test()
        {
            throw new NotImplementedException();
        }
    }
}
