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
        private readonly HotelDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IRoomCategoryRepository _roomCategoryRepository;
        private readonly IRoomRepository _roomRepository;

        public RoomController(HotelDbContext context,
            IWebHostEnvironment hostEnvironment,
            IRoomRepository roomRepository,
            IRoomCategoryRepository roomCategoryRepository)
        {
            _context = context;
            _environment = hostEnvironment;
            _roomRepository = roomRepository;
            _roomCategoryRepository = roomCategoryRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var room = _roomRepository.GetById(id);
            return View(room);
        }

        //AllRooms For a hotel - Id is hotel id - hotel // Hotel id my be stored in the token for an authontcated user
        public IActionResult AllRooms(int Id)
        {
            List<Room> rooms = _roomRepository.HotelRooms(Id);
            return View("AllRooms", rooms);
        }

        //Endpoint for add room
        public IActionResult Add()
        {
            RoomViewModel room = new RoomViewModel
            {
                Categories = _roomCategoryRepository.GetAll(),
            };

            return View("AddRoom", room);
        }

        [HttpPost]
        public IActionResult Save(RoomViewModel room)
        {
            if(ModelState.IsValid)
            {
                // Hotel id should be stored in the token for an authontcated user
                  room.HotelId = 4;
                 _roomRepository.Insert(RoomModelViewMapper.MapToRoom(room, _environment));
                _roomRepository.Save();
                return RedirectToAction("AllRooms");
            }
            room.Categories = _roomCategoryRepository.GetAll();
            return View("AddRoom", room);

        }

        //add to DB
        [HttpPost]
        public IActionResult Add(HotelViewModel hotel)
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

        public IActionResult Edit(int id)
        {
            return View("EditRoom");

        }

        public IActionResult Delete(int id)
        {
            return View("DeleteRoom");

        }
    }
}
