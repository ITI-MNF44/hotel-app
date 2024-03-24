using hotel_app.Models;
using hotel_app.Repositories;
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

  


        public IActionResult reservedRooms(int id)
        {
            var res = hotelRepository.getReservedRooms(id);
            return Json(res.ToList());
        }

    }
}
