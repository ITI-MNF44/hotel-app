using hotel_app.Models;
using hotel_app.Repositories;

namespace hotel_app.Services
{
    public interface IRoomService 
    {
        public Room GetById(int id);
        public List<RoomCategory> RoomCategories();
        public List<Room> HotelRooms(int id);
        public void Insert(Room room);
        public void Save();
    }
}
