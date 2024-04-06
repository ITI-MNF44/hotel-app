using Microsoft.AspNetCore.Mvc;

namespace hotel_app.Controllers
{
    public class HotailerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View("About");
        }
        public IActionResult Contact()
        {
            return View("Contact");
        }
    }
}
