using hotel_app.Models;
using hotel_app.Repositories;

namespace hotel_app.Services
{
    public class HotelCategoryService:IHotelCategoryService
    {
        IHotelCategoryRepository _categoryRepository;
        public HotelCategoryService(IHotelCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        //get all
        public List<HotelCategory> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }
    }
}
