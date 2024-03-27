using hotel_app.Models;
using hotel_app.Repositories;

namespace hotel_app.Services
{
    public interface IRoomService 
    {
        public Room GetById(int id);
        public Room GetById(int id, params string[] include);
        public List<RoomCategory> RoomCategories();
        public List<Room> HotelRooms(int id);
        public List<Room> AllAvailableRooms();
        public void Insert(Room room); 
        public void Update(Room room);
        public void Delete(int id);
        public void Save();
    }
}
