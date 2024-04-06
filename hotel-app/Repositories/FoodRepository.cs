using hotel_app.Models;
using Microsoft.EntityFrameworkCore;

namespace hotel_app.Repositories
{
    public class FoodRepository : GeneralRepository<Food>, IFoodRepository
    {
        HotelDbContext hotelDbContext;
        public FoodRepository(HotelDbContext DbContext) : base(DbContext)
        {
            hotelDbContext = DbContext;
        }

        public Food GetById(int id, params string[] include)
        {
            IQueryable<Food> food = hotelDbContext.Foods.Where(food => food.Id == id);

            foreach (var property in include)
            {
                food = food.Include(property);
            }

            return food.FirstOrDefault();
        }

        public List<Food> GetHotelFoods(int HotelId, params string[] include)
        {
            IQueryable<Food> foods = hotelDbContext.Foods.Where(food => food.HotelID == HotelId && food.isDeleted == false);

            foreach(var property in include)
            {
                foods = foods.Include(property);
            }

            return foods.ToList();
        }
        
        public async Task<List<Food>> GetHotelFoodsAsync(int HotelId)
        {
            return await hotelDbContext.Foods.Where(food => food.HotelID == HotelId && food.isDeleted == false).ToListAsync();
        }

    }
}
