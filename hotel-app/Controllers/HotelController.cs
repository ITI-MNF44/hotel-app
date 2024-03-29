using hotel_app.Models;
using hotel_app.Services;
using hotel_app.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace hotel_app.Controllers
{
    public class HotelController : Controller
    {
        //ask
        UserManager<ApplicationUser> usermanager;
        SignInManager<ApplicationUser> signInManager;
        IHotelCategoryService _categoryService;
        IHotelService hotelService;
        //Ctor,inject


        public HotelController(
        UserManager<ApplicationUser> usermanagerlogin,
        SignInManager<ApplicationUser> _signInManager, 
        IHotelService _HotelService, IHotelCategoryService hotelCategoryService) 
        {
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

        public IActionResult AllHotels()
        {
            var hotels = hotelService.AllHotels();
            return View("AllHotels", hotels);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserHotelRegister(RegisterUserViewModel hoteluservm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = hotelService.MapHotelUserVmToAppUser(hoteluservm);
                string userId = appUser.Id;
                IdentityResult result = await usermanager.CreateAsync(appUser, hoteluservm.Password);

              
                if (result.Succeeded)
                {
                    IdentityResult roleresult =  await usermanager.AddToRoleAsync(appUser,"Hotel");
                    if(roleresult.Succeeded)
                    {
                        List<Claim> Claims = new List<Claim>();
                        Claims.Add(new Claim(ClaimTypes.NameIdentifier, appUser.Id));
                        await signInManager.SignInWithClaimsAsync(appUser, true, Claims);
                        Hotel hotel = await hotelService.MapHotelVmToHotel(hoteluservm, userId);
                        await hotelService.RegisterInsert(hotel);
                        return RedirectToAction("Index");
                    }

                }
            }
            hoteluservm.Categories = _categoryService.GetAllCategories();
            return View("UserHotelRegister", hoteluservm);
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
                    bool Found = await usermanager.CheckPasswordAsync(AppUser, hotelVM.Passwrod);
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
            return Content("Signed Out");
        }

        public async Task<IActionResult> ReservationsInfo()
        {
            Hotel currHotel = await hotelService.GetCurrentHotel();
            if (currHotel != null)
            {
                var res = hotelService.ReservationsInfo(currHotel.Id);
                return View("DisplayHotelReservedRooms", res);
            }
            else
            {
                return Content("Error getting reservations data");
            }
           
        }

        public IActionResult getRoomReservationsDetails(int id, string roomName)
        {
            var model = hotelService.RoomReservationsDetails(id);
            ViewData["roomName"] = roomName;
            return View("RoomReservationsDetails", model);
        }



        [HttpGet]
        public IActionResult HotelProfile(int id)
        {
            HotelWithRoomsViewModel viewModel = hotelService.GetHotelWithRooms(id);
            if (viewModel.Hotel == null)
            {
                return NotFound();
            }

            return View("HotelProfile", viewModel);
        }
    }
}
