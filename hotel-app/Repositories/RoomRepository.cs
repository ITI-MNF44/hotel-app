using hotel_app.Models;
using Microsoft.EntityFrameworkCore;

namespace hotel_app.Repositories
{
    public class RoomRepository : GeneralRepository<Room>, IRoomRepository
    {
        HotelDbContext hotelDbContext;
        public RoomRepository(HotelDbContext DbContext) : base(DbContext)
        {
            hotelDbContext = DbContext;
        }

        public List<Room> HotelRooms(int Id)
        {
            return hotelDbContext.Rooms.Where(room => room.HotelId == Id)
                .Include(room => room.Hotel)
                .Include(room => room.RoomCategory).ToList();
        }
        public List<Room> AllAvailableRooms()
        {
            return hotelDbContext.Rooms
                .Include(room => room.Hotel)
                .Include(room=>room.RoomCategory).ToList();
        }

    }
}
