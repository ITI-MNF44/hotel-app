using hotel_app.Models;
using hotel_app.Repositories;
using Microsoft.EntityFrameworkCore;

namespace hotel_app.Services
{
    public class RoomService : IRoomService
    {
        readonly IRoomRepository _roomRepository;
        readonly IRoomCategoryRepository _roomCategoryRepository;
        readonly IGuestRoomRepository _guestRoomRepository;

        public RoomService(IRoomRepository roomRepository,
        IRoomCategoryRepository roomCategoryRepository,IGuestRoomRepository guestRoomRepository )
        {
            _roomRepository = roomRepository;
            _roomCategoryRepository = roomCategoryRepository;
            _guestRoomRepository = guestRoomRepository;
        }

        public Room GetById(int id)
        {
            return _roomRepository.GetById(id);
        }

        public List<Room> HotelRooms(int id)
        {
            return _roomRepository.HotelRooms(id);
        }

        public List<RoomCategory> RoomCategories()
        {
            return _roomCategoryRepository.GetAll();
        }

        public void Insert(Room room)
        {
            _roomRepository.Insert(room);
        }

        public void Save()
        {
            _roomRepository.Save();
        }

        public Room GetById(int id, params string[] include)
        {
            return _roomRepository.GetById(id, include);
        }
        public Task<Room?> GetByIdAsync(int id, params string[] include)
        {
            return _roomRepository.GetByIdAsync(id, include);
        }

        public void Update(Room room)
        {
            _roomRepository.Update(room);
        }
        public void Delete(int id)
        {
            _roomRepository.Delete(id);
        }

        public List<Room> AllAvailableRooms()
        {
            return _roomRepository.AllAvailableRooms();
        }

        public async Task<bool> isRoomAvailable(int id , int amount, DateTime startDate, DateTime endDate)
        {
            Room room = await _roomRepository.GetByIdAsync(id);
            if (room != null)
            {
                int numOfBooked = _guestRoomRepository.GetBookedRoomsCount(id, startDate, endDate);
                if (numOfBooked+amount < room.NumOfRooms)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
