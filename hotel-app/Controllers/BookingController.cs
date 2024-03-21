using Microsoft.AspNetCore.Mvc;

namespace hotel_app.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
