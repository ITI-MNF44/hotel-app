using hotel_app.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace hotel_app.Repositories
{
    public class HotelCategoryRepository : GeneralRepository<HotelCategory>, IHotelCategoryRepository
    {
        private readonly dbContext _dbContext;
        public HotelCategoryRepository(dbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<HotelCategory> GetAll()
        {
            return _dbContext.Set<HotelCategory>().ToList();
        }
    }
}
