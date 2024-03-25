using hotel_app.Models;
using Microsoft.EntityFrameworkCore;

namespace hotel_app.Repositories
{
    public class GuestRepository : GeneralRepository<Guest>, IGuestRepository
    {
        HotelDbContext DbContext;
        public GuestRepository(HotelDbContext _DbContext) : base(_DbContext)
        {
            DbContext = _DbContext;
        }

        public void test_func()
        {
            throw new NotImplementedException();
        }

        public Task<Guest> GetGuestByUserId(string userId)
        {
            return DbContext.Guests.FirstOrDefaultAsync(g => g.UserId == userId);
        }


    }
}
