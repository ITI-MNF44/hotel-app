using hotel_app.Models;
using hotel_app.Repositories;
using hotel_app.Services;
using hotel_app.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace hotel_app.Controllers
{
    public class RoomController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IRoomService _roomService;
        private readonly IHotelService _hotelService;

        public RoomController(
            IWebHostEnvironment hostEnvironment,
            IRoomService roomService,
            IHotelService hotelService)
        {
            _environment = hostEnvironment;
            _roomService = roomService;
            _hotelService = hotelService;
        }
        public IActionResult Index()
        {
            var rooms = _roomRepository.AllAvailableRooms();
            return View(rooms);
        }

        public IActionResult Details(int id)
        {
            var room = _roomService.GetById(id);
            return View(room);
        }

        public async Task<IActionResult> All()
        {
            Hotel hotel = await _hotelService.GetCurrentHotel();
            List<Room> rooms = _roomService.HotelRooms(hotel.Id);

            return View("AllRooms", rooms);
        }

        public IActionResult Add()
        {
            RoomViewModel room = new RoomViewModel
            {
                Categories = _roomService.RoomCategories(),
            };

            return View("AddRoom", room);
        }

        [HttpPost]
        public async Task<IActionResult> Save(RoomViewModel room)
        {
            if(ModelState.IsValid)
            {
                Hotel hotel = await _hotelService.GetCurrentHotel();
                room.HotelId = hotel.Id;
                room.IsDeleted = false;
                room.CreatedDate = DateTime.Now.Date;

                _roomService.Insert(RoomModelViewMapper.MapToRoom(room, _environment));
                _roomService.Save();

                return RedirectToAction("All");
            }

            room.Categories = _roomService.RoomCategories();

            return View("AddRoom", room);
        }

        public IActionResult Edit(int id)
        {
            Room room = _roomService.GetById(id, "Hotel", "RoomCategory");
            RoomViewModel? roomViewModle = RoomModelViewMapper.MapToRoomViewModel(room);
            roomViewModle.Categories = _roomService.RoomCategories();

            return View("EditRoom", roomViewModle);
        }

        [HttpPost]
        public IActionResult Update(RoomViewModel room)
        {
            //Broken
            if (ModelState.IsValid)
            {
                 _roomService.Update(RoomModelViewMapper.MapToRoom(room));
                _roomService.Save();
                return RedirectToAction("All");
            }

            return View("EditRoom", room);
        }

        public IActionResult DeleteRoom(int id)
        {
           var room = RoomModelViewMapper.MapToRoomViewModel( _roomService.GetById(id, "Hotel", "RoomCategory"));
            return View("DeleteRoom", room);

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var room = _roomService.GetById(id);
            room.isDeleted = true;
            _roomService.Update(room);
            _roomService.Save();

            return RedirectToAction("All");
        }
    }
}
