using hotel_app.Models;
using hotel_app.Services;
using hotel_app.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace hotel_app.Controllers
{
    public class GuestController : Controller
    {
        //ask
        UserManager<ApplicationUser> usermanager;
        SignInManager<ApplicationUser> signInManager;
        IGuestService GuestService;
        public GuestController(UserManager<ApplicationUser> usermanagerlogin,
            SignInManager<ApplicationUser> _signInManager, IGuestService _GuestService)
        {
            usermanager = usermanagerlogin;
            signInManager = _signInManager;
            GuestService = _GuestService;
        }
        [Authorize(Roles = "Guest")]
        public async Task<IActionResult> Index()
        {
            Guest guest = await GuestService.GetCurrentGuest();
            return Content("hello from guest index : " + guest.FirstName + " " + guest.LastName);
        }

        public IActionResult Login()
        {
            return View("GuestLoginView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginVIewModel guestVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser AppUser = await usermanager.FindByNameAsync(guestVM.Username);
                if (AppUser != null)
                {
                    bool found = await usermanager.CheckPasswordAsync(AppUser, guestVM.Passwrod);
                    if (found)
                    {
                        await signInManager.SignInAsync(AppUser, guestVM.RememberMe);
                        List<Claim> Claims = new List<Claim>();
                        Claims.Add(new Claim(ClaimTypes.NameIdentifier, AppUser.Id));
                        await usermanager.AddToRoleAsync(AppUser, "Guest");
                        await signInManager.SignInWithClaimsAsync(AppUser, guestVM.RememberMe, Claims);
                        return RedirectToAction("index", "home");
                    }

                }
                ModelState.AddModelError(string.Empty, "Username or password is incorrect");

            }
            return View("GuestLoginView", guestVM);
        }

        public async Task<IActionResult> Register()
        {
            return View("GuestRegisterView");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterGuestViewModel guestVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser AppUser = GuestService.MapGuestVmToAppUser(guestVM);
                IdentityResult result = await usermanager.CreateAsync(AppUser, guestVM.Password);
                if (result.Succeeded)
                {
                    await usermanager.AddToRoleAsync(AppUser, "Guest");
                    List<Claim> Claims = new List<Claim>();
                    Claims.Add(new Claim(ClaimTypes.NameIdentifier, AppUser.Id));
                    await signInManager.SignInWithClaimsAsync(AppUser, true, Claims);
                    Guest guest = GuestService.MapGuestVmToGuest(guestVM);
                    guest.UserId = AppUser.Id;
                    GuestService.InsertGuest(guest);
                    GuestService.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View("GuestRegisterView", guestVM);


            }
            return View("GuestRegisterView", guestVM);
        }

        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return Content("guest SignedOut");
        }

    }
}
