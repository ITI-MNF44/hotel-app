using hotel_app.Models;

namespace hotel_app.Repositories
{
    public interface IFoodRepository: IGeneralRepository<Food>
    {
        public Food test_function();
    }
}
