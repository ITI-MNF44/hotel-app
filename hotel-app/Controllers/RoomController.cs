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
        private HotelDbContext _context;
        private IWebHostEnvironment _environment;

        private RoomRepository _roomRepository;
        private IRoomCategoryRepository _roomCategoryRepository;
        private IHotelRepository _hotelRepository;
        private IRoomService _roomService;

        public RoomController(HotelDbContext context, IWebHostEnvironment hostEnvironment,IRoomService roomService, RoomRepository roomRepository, IRoomCategoryRepository roomCategoryRepository, IHotelRepository hotelRepository)
        {
            _context = context;
            _environment = hostEnvironment;
            _roomRepository = roomRepository;
            _roomCategoryRepository = roomCategoryRepository;
            _hotelRepository = hotelRepository;
            _roomService = roomService;
        }



        public IActionResult Index()
        {
            var rooms = _roomService.GetAll();
            return View(rooms);
        }

        //public IActionResult Index()
        //{
        //    var rooms = _roomRepository.GetAll();
        //    return View(rooms);
        //}

        //public IActionResult Index()
        //{
        //    List <Room> rooms = _roomRepository.GetAll();
        //    //var roomViewModels = rooms.Select(room => new RoomViewModel
        //    //{
        //    //    Name = room.Name,
        //    //    //Description = room.Description,
        //    //    HotelName = room.Hotel.Name, 
        //    //    CategoryName = room.RoomCategory.Name, 
        //    //    NumOfBeds = room.NumOfBeds,
        //    //    PricePerNight = room.PricePerNight,
        //    //    NumOfRooms = room.NumOfRooms,
        //    //    //Image = room.Image
        //    //                }).ToList();

        //    return View(rooms); // Pass roomViewModels to the view
        //}


        public IActionResult Details(int id)
        {
            var room = _roomRepository.GetById(id);
            return View(room);
        }
        //endpoint for add room
        public IActionResult AddRoom()
        {
            //not implemented yet
            return View();
        }

        //add to DB
        [HttpPost]
        public IActionResult AddRoom(HotelViewModel hotel)
        {
            //image code
            //string filename = "";
            //if (product1.photo != null)
            //{
            //    string Uploader = Path.Combine(env.WebRootPath, "images");
            //    filename = Guid.NewGuid().ToString() + "_" + product1.photo.FileName;
            //    string filepath = Path.Combine(Uploader, filename);
            //    product1.photo.CopyTo(new FileStream(filepath, FileMode.Create));
            //}
            ////
            //product p = new product()
            //{
            //    Name = product1.Name,
            //    price = product1.price,
            //    Image = filename
            //};
            ////
            //context.Products.Add(p);
            //context.SaveChanges();
            ////
            //ViewBag.success = "record added successfully";
            //not implemented yet
            return View();
        }
    }
}
