using hotel_app.Models;
using hotel_app.Repositories;
using hotel_app.Services;
using hotel_app.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;

namespace hotel_app.Controllers
{
    public class Country
    {
        public string CommonName { get; set; }
    }
    public class GuestController : Controller
    {
        //ask
        UserManager<ApplicationUser> usermanager;
        SignInManager<ApplicationUser> signInManager;
        IGuestService GuestService;
        private readonly IHttpClientFactory _clientFactory;

        IAppUserService AppUserService;
        ApplicationUser applicationUser;
        public GuestController(UserManager<ApplicationUser> usermanagerlogin,
            SignInManager<ApplicationUser> _signInManager, IGuestService _GuestService, IHttpClientFactory clientFactory, IAppUserService _AppUserService)
        {
            usermanager = usermanagerlogin;
            signInManager = _signInManager;
            GuestService = _GuestService;
            AppUserService = _AppUserService;
            _clientFactory = clientFactory;
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
                    bool Found = await usermanager.CheckPasswordAsync(AppUser, guestVM.Passwrod);
                    if (Found)
                    {
                        var userRoles = await usermanager.GetRolesAsync(AppUser);
                        if (userRoles.Contains("Guest"))
                        {
                            await signInManager.SignInAsync(AppUser, guestVM.RememberMe);
                            return RedirectToAction("AllHotels", "Hotel");
                        }
                    }

                }
                ModelState.AddModelError(string.Empty, "Username or password is incorrect");

            }
            return View("GuestLoginView", guestVM);
        }
        public async Task<List<string>> getAllCountries()
        {
                // Call the REST countries API to get the list of countries
                var client = _clientFactory.CreateClient();
                HttpResponseMessage response = await client.GetAsync("https://restcountries.com/v3.1/all");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // Parse the JSON response using JsonDocument
                using (JsonDocument doc = JsonDocument.Parse(responseBody))
                {
                    var root = doc.RootElement;
                    var countries = root.EnumerateArray();

                    // Extract country names
                    List<string> countryNames = new List<string>();
                    foreach (var country in countries)
                    {
                        string name = country.GetProperty("name").GetProperty("common").GetString();
                        countryNames.Add(name);
                    }

                    countryNames.Sort();

                    // Set the SelectList to the view model
                   return countryNames;
                }
     

            // Pass the view model to the view
        }
        public async Task<IActionResult> Register()
        {
            var guestVM = new RegisterGuestViewModel();
            guestVM.Countries = await getAllCountries();
            
            return View("GuestRegisterView", guestVM);
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
                    return RedirectToAction("AllHotels", "Hotel");
                }
                guestVM.Countries = await getAllCountries();
                return View("GuestRegisterView", guestVM);


            }
            guestVM.Countries = await getAllCountries();
            return View("GuestRegisterView", guestVM);
        }

        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public async Task< IActionResult> ReservationsHistory()
        {
            var guest = await GuestService.GetCurrentGuest();
            var id = guest.Id;
            var model = GuestService.getGuestReservations(id);
            return View("GuestReservationsHistory", model);
        }

        [HttpGet]
        [Authorize(Roles ="Guest")]
        public async Task<IActionResult> EditProfile()
        {
           var guest = await GuestService.GetCurrentGuest();
           var id = guest.Id;
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

        public async Task <IActionResult> CheckNameAvailability(string username)
        {
            ApplicationUser AppUser = await usermanager.FindByNameAsync(username);
            if (AppUser == null)
            {
                return Json(true);
            }
            return Json(false);
        }
        public async Task<IActionResult> CheckEmailAvailability(string email)
        {
            ApplicationUser AppUser = await usermanager.FindByEmailAsync(email);
            if (AppUser == null)
            {
                return Json(true);
            }
            return Json(false);
        }


    }

}

