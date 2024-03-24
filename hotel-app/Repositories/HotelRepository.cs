using hotel_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hotel_app.Repositories
{
    public class HotelRepository : GeneralRepository<Hotel>, IHotelRepository
    {
        HotelDbContext hotelDbContext;
        public HotelRepository(HotelDbContext DbContext) : base(DbContext)
        {
            hotelDbContext = DbContext;
        }

        public List<Room> getReservedRooms(int hotelId)
        {
            var hotel = hotelDbContext.Hotels
                .Where(x => x.Id == hotelId)
                .Include(x => x.Rooms)
                .FirstOrDefault();

            var res = hotel.Rooms
                .Where(room => hotelDbContext.GuestsRooms.Any(gr=>gr.RoomId== room.Id));
            return res.ToList();
        }

        public void test_func()
        {
            throw new NotImplementedException();
        }
    }
}
