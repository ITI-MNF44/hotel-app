using hotel_app.Models;
using hotel_app.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace hotel_app.Controllers
{
    public class RoomController : Controller
    {
        private HotelDbContext mycontext;
        private IWebHostEnvironment myEnvironment;

        public RoomController(HotelDbContext context, IWebHostEnvironment hostEnvironment)
        {
            mycontext = context;
            myEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
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
