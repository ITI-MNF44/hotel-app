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

        public List<GuestRoom> getGuestReservations(int id)
        {
            return DbContext.GuestsRooms.Where(x=>x.GuestId==id)
                .Include(x=>x.Room)
                .ThenInclude(x=>x.RoomCategory)
                .Include(x=>x.Food)
                .ThenInclude(x=>x.Category).OrderByDescending(x=>x.BookingDate)
                .ToList();
        }

        public Guest getGuestDetails(int id)
        {
            var res = DbContext.Guests.Where(x => x.Id == id).Include(x=>x.User).FirstOrDefault();
            return res;
        }

        public int getGuestByUserNameCount(string guestUserName)
        {
            return DbContext.Users.Where(x=>x.UserName == guestUserName).Count();
        }


        public string getGuestNamebyId(string id)
        {
            return DbContext.Users.Where(x => x.Id == id).Select(x => x.UserName).FirstOrDefault();
        }

    }
}
