using hotel_app.Models;
using Microsoft.EntityFrameworkCore;

namespace hotel_app.Repositories
{
    public class GuestRoomRepository : GeneralRepository<GuestRoom>, IGuestRoomRepository
    {
        HotelDbContext hotelDbContext;
        public GuestRoomRepository(HotelDbContext DbContext) : base(DbContext)
        {
            hotelDbContext = DbContext;
        }
        public int GetBookedRoomsCount(int roomId, DateTime startDate, DateTime endDate)
        {
            return hotelDbContext.GuestsRooms.Where(r=>r.RoomId == roomId & r.StartDate >= startDate & r.EndDate<=endDate).Count();
        }
    }
}
