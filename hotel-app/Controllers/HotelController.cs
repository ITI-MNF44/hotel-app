using hotel_app.Models;
using hotel_app.Repositories;
using hotel_app.Services;
using hotel_app.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace hotel_app.Controllers
{
    public class HotelController : Controller
    {
        //ask
        HotelDbContext mycontext;
        IWebHostEnvironment myEnvironment;
        UserManager<ApplicationUser> usermanager;
        SignInManager<ApplicationUser> signInManager;
        IHotelCategoryService _categoryService;
        IHotelService hotelService;
        //Ctor,inject
        public HotelController(HotelDbContext context, IWebHostEnvironment hostEnvironment,
            UserManager<ApplicationUser> usermanagerlogin,IHotelCategoryService hotelCategoryService ,SignInManager<ApplicationUser> _signInManager, IHotelService _HotelService) 
        {
            mycontext = context;
            myEnvironment = hostEnvironment;
            usermanager = usermanagerlogin;
            signInManager = _signInManager;
            hotelService = _HotelService;
            _categoryService = hotelCategoryService;
        }

        [Authorize(Roles = "Hotel")]
        public async Task<IActionResult> Index()
        {
            Hotel h = await hotelService.GetCurrentHotel(); 
            return Content("current hotel : "+h.Name);
        }
        //1-open registeration form 
        [HttpGet]
        public IActionResult UserHotelRegister()
        {
            var categories = _categoryService.GetAllCategories();
            var vm = new RegisterUserViewModel
            {
                Categories = categories
            };
            return View(vm);
        }
        //2-save to db
        [HttpPost]
        public async Task<IActionResult> UserHotelRegister(RegisterUserViewModel hoteluservm)
        {
            // Continue with form submission logic
            if (ModelState.IsValid)
            {
                await hotelService.RegisterInsert(hoteluservm);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var categories = _categoryService.GetAllCategories();
                hoteluservm.Categories = categories;
                return View("UserHotelRegister", hoteluservm);
            }
        }
		public IActionResult Login()
		{
			return View("HotelLoginView");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(UserLoginVIewModel hotelVM)
		{
            if (ModelState.IsValid)
            {
                ApplicationUser AppUser = await usermanager.FindByNameAsync(hotelVM.Username);
                if (AppUser != null)
                {
                    bool Found = hotelVM.Passwrod.Equals(AppUser.PasswordHash);
                    if (Found)
                    {
                        await signInManager.SignInAsync(AppUser, hotelVM.RememberMe);
                        List<Claim> Claims = new List<Claim>();
                        Claims.Add(new Claim(ClaimTypes.NameIdentifier, AppUser.Id));
                        await usermanager.AddToRoleAsync(AppUser, "Hotel");
                        await signInManager.SignInWithClaimsAsync(AppUser, hotelVM.RememberMe, Claims);
                        return RedirectToAction("index", "home");
                    }

                }
                ModelState.AddModelError(string.Empty, "Username or password is incorrect");

            }
            return View("HotelLoginView", hotelVM);
		}

        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return Content("SignedOut");
        }

        public IActionResult ReservationsInfo(int id)
        {
            var res = hotelService.ReservationsInfo(id);
            return View("DisplayHotelReservedRooms", res);
        }

        public IActionResult getRoomReservationsDetails(int id, string roomName)
        {
            var model = hotelService.RoomReservationsDetails(id);
            ViewData["roomName"] = roomName;
            return View("RoomReservationsDetails", model);
        }


    }
}
