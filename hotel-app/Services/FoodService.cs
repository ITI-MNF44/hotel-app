using hotel_app.Models;
using hotel_app.Repositories;

namespace hotel_app.Services
{
    public class FoodService : IFoodService
    {
        readonly IFoodCategoryRepository _foodCategoryRepository;
        readonly IFoodRepository _foodRepository;
        public FoodService(IFoodCategoryRepository foodCategoryRepository, IFoodRepository foodRepository)
        {
            _foodCategoryRepository = foodCategoryRepository;
            _foodRepository = foodRepository;

        }

        public Food GetById(int id, params string[] include)
        {
           return _foodRepository.GetById(id, include);
        }
        public void Delete(int id)
        {
            _foodRepository.Delete(id);
        }

        public List<FoodCategory> GetFoodCategories()
        {
            return _foodCategoryRepository.GetAll();
        }

        public List<Food> GetHotelFoods(int HotelId, params string[] include)
        {
            return _foodRepository.GetHotelFoods(HotelId, include);
        }

        public void Insert(Food food)
        {
            _foodRepository.Insert(food);
        }

        public void Update(Food food)
        {
            _foodRepository.Update(food);
        }
        public void Save()
        {
            _foodRepository.Save();
        }

    }
}
