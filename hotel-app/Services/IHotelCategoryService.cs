using hotel_app.Models;

namespace hotel_app.Services
{
    public interface IHotelCategoryService
    {
        public List<HotelCategory> GetAllCategories();
    }
}
