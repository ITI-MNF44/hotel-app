using hotel_app.Models;
using hotel_app.Repositories;

namespace hotel_app.Services
{
    public class RoomService : GeneralRepository<Room>, IRoomService
    {
        private readonly RoomRepository _roomRepository;


        public RoomService(HotelDbContext DbContext, RoomRepository roomRepository): base(DbContext)
        {
            _roomRepository = roomRepository;
        }
        public IEnumerable<Room> GetAll()
        {
            return _roomRepository.GetAll();
        }

    }
}
