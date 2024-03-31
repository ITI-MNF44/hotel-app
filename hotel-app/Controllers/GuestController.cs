using hotel_app.Models;
using hotel_app.Repositories;
using hotel_app.Services;
using hotel_app.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace hotel_app.Controllers
{
    public class GuestController : Controller
    {
        //ask
        UserManager<ApplicationUser> usermanager;
        SignInManager<ApplicationUser> signInManager;
        IGuestService GuestService;
        IAppUserService AppUserService;
        ApplicationUser applicationUser;
        public GuestController(UserManager<ApplicationUser> usermanagerlogin,
            SignInManager<ApplicationUser> _signInManager, IGuestService _GuestService, IAppUserService _AppUserService)
        {
            usermanager = usermanagerlogin;
            signInManager = _signInManager;
            GuestService = _GuestService;
            AppUserService = _AppUserService;
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

        public IActionResult ReservationsHistory(int guestId)
        {
            var model = GuestService.getGuestReservations(guestId);
            return View("GuestReservationsHistory", model);
        }

        [HttpGet]
        public IActionResult EditProfile(int id)
        {

           var model =  GuestService.GetUserProfile(id);
            applicationUser = AppUserService.GetUserById(model.UserId);


            return View("EditProfile", model);
        }

        [HttpPost]
        public IActionResult EditProfile(UserProfileViewModel userProfileViewModel)
        {
        
           GuestService.EditGuestProfile(userProfileViewModel);
            ViewData["msg"] = "data updated";
            return  View("EditProfile", userProfileViewModel);
        }

        public IActionResult validateUserName(string UserName, string UserId)
        {
            int count = GuestService.getGuestByUserNameCount(UserName);
            if(count== 0)
            {
                return Json(true);
            }

            string name = GuestService.getGuestUserNameById(UserId);
            if (name == UserName)
            {
                return Json(true);
            }
            return Json(false);
        }

        public IActionResult validateEmail(string UserEmail, string UserId)
        {
            int count = AppUserService.GetCount(x => x.Email == UserEmail);
            if (count == 0)
            {
                return Json(true);
            }
            string email = AppUserService.GetUserById(UserId).Email;
            if (UserEmail == email)
            {
                return Json(true);
            }
            return Json(false);
        }

        public IActionResult validateConfirmPassword(string ConfirmPassword, string NewPassword)
        {
            if(NewPassword == ConfirmPassword)
            {
                return Json(true);
            }
            return Json(false);
        }

        public async Task<IActionResult> ValidateCurrentPassword(string password, string userId)
        {

            if(password=="")
                return Json(true);


            var user = await usermanager.FindByIdAsync(userId);

            if (user == null)
            {
                // Handle the case where user is not found
                return NotFound();
            }

            var hashedPassword = user.PasswordHash;
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var result = passwordHasher.VerifyHashedPassword(user, hashedPassword, password);


            // Check the result
            if (result == PasswordVerificationResult.Success)
            {
                // The passwords match
                return Json(true);
            }
            else
            {
                // The passwords do not match
                return Json(false);
            }
        }
    }

}

