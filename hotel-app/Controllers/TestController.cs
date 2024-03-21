using hotel_app.Models;
using hotel_app.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace hotel_app.Controllers
{
    public class TestController : Controller
    {
        private readonly IGuestRepository guestRepository;

        public TestController(IGuestRepository _guestRepository)
        {
            guestRepository = _guestRepository;
        }

        public IActionResult Index()
        {
            string res = guestRepository.GetAll().Count().ToString();
            return Content("hello from TestController: num of guest = "+res);
        }
    }
}
