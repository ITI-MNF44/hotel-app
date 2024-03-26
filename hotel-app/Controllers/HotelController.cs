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
        IHotelService hotelService;
        IHotelRepository hotelRepository;
        IHotelCategoryRepository hotelCategoryRepository;
        //Ctor,inject
        public HotelController(HotelDbContext context, IWebHostEnvironment hostEnvironment,
            UserManager<ApplicationUser> usermanagerlogin, SignInManager<ApplicationUser> _signInManager, 
            IHotelService _HotelService, IHotelRepository _hotelRepository, IHotelCategoryRepository _hotelCategoryRepository) 
        {
            mycontext = context;
            myEnvironment = hostEnvironment;
            usermanager = usermanagerlogin;
            signInManager = _signInManager;
            hotelService = _HotelService;
            hotelRepository = _hotelRepository;
            hotelCategoryRepository = _hotelCategoryRepository;
        }

        [Authorize(Roles = "Hotel")]
        public async Task<IActionResult> Index()
        {
            Hotel h = await hotelService.GetCurrentHotel(); 
            return Content("current hotel : "+h.Name);
        }

        public IActionResult AllHotels()
        {
            var hotels = hotelRepository.AllHotels();
            return View("AllHotels", hotels);
        }

        //1-open registeration form 
        [HttpGet]
        public IActionResult UserHotelRegister()
        {
            var categories = mycontext.HotelsCategories
                                      .Select(c => new SelectListItem
                                      {
                                          Text = c.Name,
                                          Value = c.Id.ToString()
                                      })
                                      .ToList();

            if (categories == null)
            {
                categories = new List<SelectListItem>(); 
            }

            var hoteluservm = new RegisterUserViewModel
            {
                Categories = categories
            };

            return View(hoteluservm);
        }
        //2-save to db
        [HttpPost]
        public async Task<IActionResult> UserHotelRegister(RegisterUserViewModel hoteluservm)
        {
            //first insert to user table
            if(ModelState.IsValid == true)
            {
                //user first
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = hoteluservm.UserName,
                    Email = hoteluservm.Email,
                    PasswordHash=hoteluservm.Password
                };
                IdentityResult userCreationResult = await usermanager.CreateAsync(user); //hashed
                if(userCreationResult.Succeeded)
                {
                    //get userid
                    string userId = user.Id;
                    //save the rest of other data 
                    Hotel hotel = new Hotel()
                    {
                        Name = hoteluservm.Name,
                        Description=hoteluservm.Description,
                        Country=hoteluservm.Country,
                        City=hoteluservm.City,
                        Address=hoteluservm.Address,
                        StarRating=hoteluservm.StarRating,
                        Category=hoteluservm.Category,
                        CreatedDate=DateTime.Now,
                        Latitude=hoteluservm.Latitude,
                        Longitude=hoteluservm.Longitude,
                        Image=hoteluservm.Image,
                        UserId = userId,
                    };
                    //save
                    mycontext.Hotels.Add(hotel);
                    await mycontext.SaveChangesAsync();
                    return RedirectToAction("");

                }
                else
                {
                    foreach(var item in userCreationResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            else
            {
                return View("UserHotelRegister",hoteluservm);
            }
            return View();
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


    }
}
