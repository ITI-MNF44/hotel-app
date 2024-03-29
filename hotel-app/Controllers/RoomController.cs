using hotel_app.Models;
using hotel_app.Repositories;
using hotel_app.Services;
using hotel_app.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace hotel_app.Controllers
{
    public class RoomController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IRoomService _roomService;
        private readonly IHotelService _hotelService;
        private readonly IFoodService _foodService;

        public RoomController(
            IWebHostEnvironment hostEnvironment,
            IRoomService roomService,
            IHotelService hotelService,
            IFoodService foodService)
        {
            _environment = hostEnvironment;
            _roomService = roomService;
            _hotelService = hotelService;
            _foodService = foodService;
        }
        public IActionResult Index()
        {
            var rooms = _roomService.AllAvailableRooms();
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
        [Authorize]
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

        [Authorize]
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
            if (ModelState.IsValid)
            {
                Room oldRoom = _roomService.GetById(room.Id, "Hotel", "RoomCategory");
                room.HotelId=oldRoom.HotelId;
                room.CreatedDate = oldRoom.CreatedDate;
                
                if ( room.Image == null)
                {
                    Room updatedRoom = RoomModelViewMapper.MapToRoom(room);
                    updatedRoom.Image = oldRoom.Image;
                    _roomService.Update(updatedRoom);
                }
                else
                {
                    Room updatedRoom = RoomModelViewMapper.MapToRoom(room, _environment);
                    _roomService.Update(updatedRoom);
                }

                _roomService.Save();
                return RedirectToAction("All");
            }

            return View("EditRoom", room);
        }

        [Authorize]
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


        [HttpGet]
       public async Task<IActionResult> BookDetails(int id)
        {
            BookingDetailsViewModel bookingVm = await _roomService.GetBookingRoomVM(id);
            //ViewBag.foods = _foodService.GetHotelFoods(room.HotelId).ToList();
            return View("Details", bookingVm);
        }

        [HttpPost]
        public IActionResult ConfirmBook(BookingDetailsViewModel bookingVM)
        {
            return View("bookingSummary");
        }
       
        public async Task<IActionResult> CheckRoomAvailable(int Id,int amount,DateTime startDate , DateTime endDate)
       {
            try
            {
               
               bool result = await _roomService.isRoomAvailable(Id, amount, startDate, endDate);
               return Json(new { data = result });
            }
            catch (Exception ex)
            {
                // Log the exception or return an appropriate error response.
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
