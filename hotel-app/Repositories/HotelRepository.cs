using HelperClass;
using hotel_app.Models;
using hotel_app.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

namespace hotel_app.Repositories
{
    public class HotelRepository : GeneralRepository<Hotel>, IHotelRepository
    {
        HotelDbContext hotelDbContext;
        IRoomRepository roomRepository;
        public HotelRepository(HotelDbContext DbContext, IRoomRepository _roomRepository) : base(DbContext)
        {
            hotelDbContext = DbContext;
            roomRepository = _roomRepository;
        }

        public List<Room> getReservationDetails(int hotelId)
        {
            // get hotel id == hotelId
            // load rooms navigator property of hotel,
            // load  GuestRooms navigator property of rooms navigator property 
            // load  RoomCategory navigator property of rooms navigator property 

            var hotel = hotelDbContext.Hotels
                .Where(x => x.Id == hotelId)
                .Include(x => x.Rooms!)
                    .ThenInclude(g => g!.GuestRooms)
                .Include(x => x.Rooms!)
                    .ThenInclude(c => c.RoomCategory)
                .FirstOrDefault();

            //get hotel rooms that has a reservation and ensure thatis current reservation
            var rooms = hotel.Rooms
            .Where(room => hotelDbContext.GuestsRooms.Any(gr => gr.RoomId == room.Id && gr.EndDate >= TimeHelperClass.getCurrTime()));
            return rooms.ToList();
        }

        public List<GuestRoom> getRoomReservationDetails(int id)
        {
            var room = roomRepository.GetById(id);

            var Reservations = hotelDbContext.Entry(room).Collection(b => b.GuestRooms)
                .Query()
               .Include(gr => gr.Guest)
               .Include(gr => gr.Room)
               .Include(gr=>gr.Food)
               .ThenInclude(x=>x.Category)
                .Where(d => d.EndDate > TimeHelperClass.getCurrTime()).ToList();

            return Reservations;
        }
    }
}
