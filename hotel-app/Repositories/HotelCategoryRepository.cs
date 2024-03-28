using hotel_app.Models;
using Microsoft.EntityFrameworkCore;

namespace hotel_app.Repositories
{
    public class HotelCategoryRepository : GeneralRepository<HotelCategory>, IHotelCategoryRepository
    {
        private readonly HotelDbContext _dbContext;

        public HotelCategoryRepository(HotelDbContext DbContext) : base(DbContext)
        {
            _dbContext = DbContext;
        }
        //
     
    }

}
