using HelperClass;
using hotel_app.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using hotel_app.Models;
using hotel_app.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace hotel_app.Repositories
{
    public class HotelRepository : GeneralRepository<Hotel>, IHotelRepository
    {
        HotelDbContext DbContext;
        IRoomRepository roomRepository;
        public HotelRepository(HotelDbContext _DbContext, IRoomRepository _roomRepository) : base(_DbContext)
        {
            DbContext = _DbContext;
            roomRepository = _roomRepository;
        }

        Task<Hotel> IHotelRepository.GetHotelByUserId(string userId)
        {
            return DbContext.Hotels.FirstOrDefaultAsync(h => h.UserId == userId);
        }

        public List<Room> getReservationsDetails(int hotelId)
        {
            // get hotel id == hotelId
            // load rooms navigator property of hotel,
            // load  GuestRooms navigator property of rooms navigator property 
            // load  RoomCategory navigator property of rooms navigator property 

            var hotel = DbContext.Hotels
                .Where(x => x.Id == hotelId)
                .Include(x => x.Rooms!)
                    .ThenInclude(g => g!.GuestRooms)
                .Include(x => x.Rooms!)
                    .ThenInclude(c => c.RoomCategory)
                .FirstOrDefault();

            //get hotel rooms that has a reservation and ensure that is a current reservation
            var rooms = hotel.Rooms
            .Where(room => DbContext.GuestsRooms.Any(gr => gr.RoomId == room.Id && gr.EndDate >= TimeHelperClass.getCurrTime()));
            return rooms.ToList();
        }

        public List<Hotel> AllHotels()
        {
            return DbContext.Hotels
                .Include(hotel => hotel.HotelCategory).ToList();
        }

        public List<GuestRoom> getRoomReservationsDetails(int id)
        {
            // get room with id = id
            var room = roomRepository.GetById(id);

            // for retrieved room
            // load navigation property GuestRooms
            // load navigation properties (Guest, Room, Food) of navigation property GuestRooms
            // load navigation property Category of Food
            var Reservations = DbContext.Entry(room).Collection(b => b.GuestRooms)
                .Query()
               .Include(gr => gr.Guest)
               .Include(gr => gr.Room)
               .Include(gr => gr.Food)
               .ThenInclude(x => x.Category)
                .Where(d => d.EndDate > TimeHelperClass.getCurrTime()).ToList();

            return Reservations;
        }
    }
}
