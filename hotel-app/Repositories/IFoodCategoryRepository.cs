using hotel_app.Models;

namespace hotel_app.Repositories
{
    public interface IFoodCategoryRepository : IGeneralRepository<FoodCategory>
    {
        public void test();
    }
}
