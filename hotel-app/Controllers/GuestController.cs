using hotel_app.Models;
using hotel_app.Services;
using hotel_app.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace hotel_app.Controllers
{
    public class GuestController : Controller
    {
        //ask
        HotelDbContext mycontext;
        IWebHostEnvironment myEnvironment;
        UserManager<ApplicationUser> usermanager;
        SignInManager<ApplicationUser> signInManager;
        IGuestService GuestService;
        public GuestController( UserManager<ApplicationUser> usermanagerlogin, 
            SignInManager<ApplicationUser> _signInManager, IGuestService _GuestService)
        {
            myEnvironment = hostEnvironment;
            usermanager = usermanagerlogin;
            signInManager = _signInManager;
            GuestService = _GuestService;
        }
        public IActionResult Index()
        {
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
    }
}
