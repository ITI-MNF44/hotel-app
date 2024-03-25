using hotel_app.Models;
using hotel_app.Repositories;
using hotel_app.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace hotel_app.Controllers
{
    public class TestController : Controller
    {
        private readonly IGuestRepository guestRepository;
        private readonly IHotelRepository hotelRepository;

        public TestController(IGuestRepository _guestRepository, IHotelRepository _hotelRepository)
        {
            guestRepository = _guestRepository;
            hotelRepository = _hotelRepository;
        }

        public IActionResult Index()
        {
            string res = guestRepository.GetAll().Count().ToString();
            return Content("hello from TestController: num of guest = "+res);
        }


        public IActionResult ReservationInfo(int id)
        {
            var res = hotelRepository.getReservationDetails(id);
            return View("DisplayHotelReservedRooms", res);
        }

        public IActionResult getRoomReservationDetails(int id, string roomName)
        {
            var RoomReservations = hotelRepository.getRoomReservationDetails(id);

            var model = hotelRepository.getRoomReservationDetails(id).Select(x => new RoomGuestReservationVM()
            {
                Full_name = x.Guest.FirstName + " " + x.Guest.LastName,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                BookingDate = x.BookingDate,
                Rooms_amount = x.RoomsAmount,
                RoomPrice = x.Room.PricePerNight,
                FoodAmount = x.FoodAmount,
                FoodCategory =  x.Food==null?null:x.Food.Category.Name,
                FoodName = x.Food == null?null:x.Food.Name,
                FoodPrice = x.Food == null?null:x.Food.PricePerPerson,

            }).ToList();
            ViewData["roomName"] = roomName;
            return View("RoomReservationsDetails",model);
        }

    }
}
