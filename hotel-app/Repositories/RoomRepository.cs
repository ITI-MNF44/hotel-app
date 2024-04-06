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

        public Room? GetById(int id, params string[] include)
        {
            IQueryable<Room> room = hotelDbContext.Rooms.Where(room => room.Id == id);

            foreach(var includeProperty in include)
            {
                room = room.Include(includeProperty);
            }

            return room.AsNoTracking().FirstOrDefault();
        }
        
        public List<Room> HotelRooms(int Id)
        {
            return hotelDbContext.Rooms.Where(room => room.HotelId == Id && room.isDeleted == false)
                .Include(room => room.Hotel)
                .Include(room => room.RoomCategory)
                .AsNoTracking()
                .ToList();
        }
        public List<Room> AllAvailableRooms()
        {
            return hotelDbContext.Rooms
                .Include(room => room.Hotel)
                .Include(room=>room.RoomCategory).ToList();
        }

        public async Task<Room?> GetByIdAsync(int id, params string[] include)
        {
            IQueryable<Room> roomQuery = hotelDbContext.Rooms.Where(room => room.Id == id);

            foreach (var includeProperty in include)
            {
                roomQuery = roomQuery.Include(includeProperty);
            }

            return await roomQuery.AsNoTracking().FirstOrDefaultAsync();
        }

    }
}
