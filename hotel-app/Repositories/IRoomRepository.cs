using hotel_app.Models;
using System.Collections.Generic;

namespace hotel_app.Repositories
{
    public interface IRoomRepository:IGeneralRepository<Room>
    {
        public List<Room> HotelRooms(int Id);
    }
}
