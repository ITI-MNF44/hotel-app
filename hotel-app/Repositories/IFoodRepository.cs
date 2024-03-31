using hotel_app.Models;

namespace hotel_app.Repositories
{
    public interface IFoodRepository: IGeneralRepository<Food>
    {
        public Food GetById(int id, params string[] include);
        public List<Food> GetHotelFoods(int HotelId, params string[] include);
        public Task<List<Food>> GetHotelFoodsAsync(int HotelId);

    }
}
