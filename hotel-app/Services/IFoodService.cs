using hotel_app.Models;

namespace hotel_app.Services
{
    public interface IFoodService
    {
        public Food GetById(int id, params string[] include);
        public List<FoodCategory> GetFoodCategories();
        public List<Food> GetHotelFoods(int HotelId, params string[] include);
        public void Insert(Food food);
        public void Update(Food food);
        public void Delete(int id);
        public void Save();
        public Task<List<Food>> GetHotelFoodsAsync(int HotelId);


    }
}
