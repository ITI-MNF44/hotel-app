using Microsoft.AspNetCore.Mvc;

namespace hotel_app.Controllers
{
	public class HotelController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View("Register");
		}

	}
}
