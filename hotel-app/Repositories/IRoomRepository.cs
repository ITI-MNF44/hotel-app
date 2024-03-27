using hotel_app.Models;
using System.Collections.Generic;

namespace hotel_app.Repositories
{
    public interface IRoomRepository:IGeneralRepository<Room>
    {
        public Room GetById(int id, string[] include);
        public List<Room> HotelRooms(int Id);
      
        public List<Room> AllAvailableRooms();
    }
}
