using hotel_app.Models;
<<<<<<< Updated upstream
using Microsoft.AspNetCore.Mvc.Rendering;
=======
>>>>>>> Stashed changes
using Microsoft.EntityFrameworkCore;

namespace hotel_app.Repositories
{
    public class HotelCategoryRepository : GeneralRepository<HotelCategory>, IHotelCategoryRepository
    {
        private readonly dbContext _dbContext;
        public HotelCategoryRepository(dbContext dbContext) : base(dbContext)
        {
<<<<<<< Updated upstream
            _dbContext = dbContext;
        }

        public List<HotelCategory> GetAll()
        {
            return _dbContext.Set<HotelCategory>().ToList();
=======

>>>>>>> Stashed changes
        }
        public List<HotelCategory> GetAllCategories()
        {
            return GetAll();
        }

    }
}
